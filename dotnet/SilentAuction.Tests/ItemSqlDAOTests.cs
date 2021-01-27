using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using Capstone.DAO;
using Capstone.Models;
using System.IO;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SilentAuction.Tests
{
    [TestClass]
    public class ItemSqlDAOTests
    {
        private TransactionScope tran;
        private string connectionString = @"Server=.\sqlexpress;database=final_capstone; trusted_connection=true;";

        [TestInitialize]
        public void StartupTests()
        {
            tran = new TransactionScope();
            string sqlTestScript = File.ReadAllText("testscript.sql");
        
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlTestScript, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [TestCleanup]
        public void CleanupTests()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllItemsReturnsCorrectAmount()
        {
            ItemSqlDAO dao = new ItemSqlDAO(connectionString);
            List<Item> result = dao.GetAllItems();
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void GetAllItemsHasCorrectNumberOfCategoriesInListOfCategories()
        {
            ItemSqlDAO dao = new ItemSqlDAO(connectionString);
            List<Item> result = dao.GetAllItems();
            int[] testCounts = new int[] { 3, 2, 1, 2, 2};
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(testCounts[i], result[i].Categories.Count);
            }
        }

        // TODO: test 
    }
}
