using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CategorySqlDAO : ICategoryDAO
    {
        private readonly string connectionString;

        public CategorySqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<Category> GetAllCategories()
        {
            List<Category> returnCategories = new List<Category>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(" Select * From category", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Category category = RowToObject(reader);
                        returnCategories.Add(category);
                    }
                    return returnCategories;

                }
            }
            catch (SqlException)
            {
                throw;
            }
        }


        private static Category RowToObject(SqlDataReader rdr)
        {
            Category category = new Category();
            category.Category_ID = Convert.ToInt32(rdr["category_id"]);
            category.Name = Convert.ToString(rdr["name"]);

            return category;
        }
    }
}
