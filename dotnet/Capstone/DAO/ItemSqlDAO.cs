using Capstone.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class ItemSqlDAO : IItemDAO
    {
        private readonly string connectionString;

        public ItemSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Item> GetAllItems()
        {
            List<Item> returnItems = new List<Item>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("GetAllItems", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Item item = RowToObject(reader);
                        returnItems.Add(item);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        int item_ID = Convert.ToInt32(reader["item_id"]);
                        string name = Convert.ToString(reader["name"]);
                        // TODO: find the item with the id of 'item_id'
                        Item foundItem = returnItems.Find(item => item.Item_ID == item_ID);
                        // TODO: add name to list of categories
                        foundItem.Categories.Add(name);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        ReturnBid bid = BidRowToObject(reader);
                        int item_ID = Convert.ToInt32(reader["item_ID"]);
                        // Attach bids to each item as list
                        Item foundBid = returnItems.Find(item => item.Item_ID == item_ID);
                        foundBid.Bids.Add(bid);
                    }
                    return returnItems;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public Item GetSingleItem(int item_Id)
        {
            Item returnItem = new Item();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand($"Select * From Item WHERE item_id = @item_Id order by title; " +
                                                     "Select * From item_category IC " +
                                                     "JOIN Category C on IC.category_id = c.category_id WHERE IC.item_id = @item_Id; " +
                                                     "SELECT  * From bid " +
                                                     "JOIN item on bid.item_id = item.item_id " +
                                                     "JOIN users on users.user_id = bid.user_id WHERE item.item_id = @item_Id Order by amount desc", conn);
                    cmd.Parameters.AddWithValue("@item_Id", item_Id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Item item = RowToObject(reader);
                        returnItem = item;
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        int item_ID = Convert.ToInt32(reader["item_id"]);
                        string name = Convert.ToString(reader["name"]);
                        // TODO: find the item with the id of 'item_id'
                        Item foundItem = returnItem;
                        // TODO: add name to list of categories
                        foundItem.Categories.Add(name);
                    }
                    reader.NextResult();
                    while (reader.Read())
                    {
                        ReturnBid bid = BidRowToObject(reader);
                        int item_ID = Convert.ToInt32(reader["item_ID"]);
                        // Attach bids to each item as list
                        Item foundBid = returnItem;
                        foundBid.Bids.Add(bid);
                    }
                    return returnItem;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Item> GetCurrentUserWinnings(int userID)
        {
            List<Item> result = new List<Item>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("EndForUser", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userID", userID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Item item = RowToObject(reader);
                        result.Add(item);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return result;
        }

        public Item AddNewItem(Item item)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO item (item_id, donor, auction_id, title, subtitle, description, pic, starting_bid) VALUES (@item_id, @donor, @auction_id, @title, @subtitle, @description, @pic, @starting_bid)", conn);
                    cmd.Parameters.AddWithValue("@item_id", item.Item_ID);
                    cmd.Parameters.AddWithValue("@donor", item.Donor);
                    cmd.Parameters.AddWithValue("@auction_id", item.Auction_ID);
                    cmd.Parameters.AddWithValue("@title", item.Title);
                    cmd.Parameters.AddWithValue("@subtitle", item.Subtitle);
                    cmd.Parameters.AddWithValue("@description", item.Description);
                    cmd.Parameters.AddWithValue("@pic", item.Pic);
                    cmd.Parameters.AddWithValue("@starting_bid", item.Starting_Bid);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return GetSingleItem(item.Item_ID);
        }

        private static Item RowToObject(SqlDataReader rdr)
        {
            Item item = new Item();
            item.Item_ID = Convert.ToInt32(rdr["item_id"]);
            item.Auction_ID = Convert.ToInt32(rdr["auction_id"]);
            item.Donor = Convert.ToString(rdr["donor"]);
            item.Subtitle = Convert.ToString(rdr["subtitle"]);
            item.Title = Convert.ToString(rdr["Title"]);
            item.Pic = Convert.ToString(rdr["pic"]);
            item.Description = Convert.ToString(rdr["description"]);
            item.Starting_Bid = Convert.ToDecimal(rdr["starting_bid"]);
            item.Categories = new List<string>();
            item.Bids = new List<ReturnBid>();
            // item.Category.Add(Convert.ToString(rdr["name"]));
            // TODO: rename category name on database
            return item;

        }
        private static ReturnBid BidRowToObject(SqlDataReader rdr)
        {
            //string wholeAddress = Convert.ToString(rdr["username"]);
            //string bit = wholeAddress.Substring(0, 2);
            //string emailProvider = wholeAddress.Substring(wholeAddress.IndexOf("@"), 2);
            //string anonymousUser = bit + "**" + emailProvider + "****.com";


            ReturnBid bid = new ReturnBid();
            bid.Bid_ID = Convert.ToInt32(rdr["bid_id"]);
            bid.Item_ID = Convert.ToInt32(rdr["item_ID"]);
            bid.Amount = Convert.ToDecimal(rdr["amount"]);
            bid.User_Name = Convert.ToString(rdr["username"]);
            bid.Time_Placed = Convert.ToString(rdr["time_placed"]);
            return bid;
        }
    }
}