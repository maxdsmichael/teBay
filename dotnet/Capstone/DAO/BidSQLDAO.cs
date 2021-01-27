using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


namespace Capstone.DAO
{
    public class BidSqlDAO : IBidDAO
    {
        private readonly string connectionString;

        public BidSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Bid> GetBidsByItem(int id)
        {
            List<Bid> bidsByIDs = new List<Bid>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select * from bid join item on item.item_id = bid.item_id where item.item_id = @item_id", conn);
                    cmd.Parameters.AddWithValue("@item_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Bid bid = RowToObject(reader);
                        bidsByIDs.Add(bid);
                    }
                    return bidsByIDs;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public ReturnBid AddBid(Bid bid) 
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string timeStamp = DateTime.Now.ToString("MM/dd/yy HH:mm");
                    SqlCommand cmd = new SqlCommand($"INSERT INTO bid (item_id, user_id, amount, time_placed) VALUES (@item_id, @user_id, @bid_amount, @now); Select @@IDENTITY;", conn);
                    cmd.Parameters.AddWithValue("@item_id", bid.Item_ID);
                    cmd.Parameters.AddWithValue("@user_id", bid.User_ID);  
                    cmd.Parameters.AddWithValue("@bid_amount", bid.Amount);
                    cmd.Parameters.AddWithValue("@now", timeStamp);
                    int newID = Convert.ToInt32(cmd.ExecuteScalar());

                    ReturnBid returnedBid = new ReturnBid();
                    returnedBid.Amount = bid.Amount;
                    returnedBid.Item_ID = bid.Item_ID;
                    returnedBid.Time_Placed = timeStamp;
                    returnedBid.Bid_ID = newID;
                    
                    return returnedBid; 
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public decimal GetHighestBidAmountForItem(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select isnull(MAX(amount), 0) from bid where item_id = @item_id", conn);
                    cmd.Parameters.AddWithValue("@item_id", id);
                    decimal topAmount = Convert.ToDecimal(cmd.ExecuteScalar());

                    return topAmount;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Bid> GetBidHistoryForUser(int user_ID)
        {
            const string query = "Select * from bid where user_id = @userID Order By time_placed desc";
            List<Bid> result = new List<Bid>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userID", user_ID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(RowToObject(reader));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public List<Bid> GetHighestUserBidForEachItem(int user_ID)
        {
            const string query = "select * from bid Join(Select item_id, Max(amount) as maxAmount from bid where user_id = @user_id group by item_id) as topBids on bid.item_id = topBids.item_id and bid.amount = topBids.maxAmount";
            List<Bid> result = new List<Bid>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user_id", user_ID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(RowToObject(reader));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result; 
        }


        public List<Bid> GetHighestBidForAllItems()
        {
            List<Bid> result = new List<Bid>();
            const string query = "Select * from bid " +
                "Join(Select item_id, Max(amount) as maxAmount from bid Group By item_id) as topBids " +
                "on bid.item_id = topBids.item_id and bid.amount = topBids.maxAmount";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(RowToObject(reader));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
       
        private static Bid RowToObject(SqlDataReader rdr)
        {
            DateTime date = Convert.ToDateTime(rdr["time_placed"]);
            Bid bid = new Bid();
            bid.Amount = Convert.ToDecimal(rdr["amount"]);
            bid.Bid_ID = Convert.ToInt32(rdr["bid_id"]);
            bid.Item_ID = Convert.ToInt32(rdr["Item_id"]);
            bid.Time_Placed = date.ToString("MM/dd/yy HH:mm");
            bid.User_ID = Convert.ToInt32(rdr["user_id"]);
            return bid;
        }
    }
}
