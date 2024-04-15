using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LoveSync_DBProject.Pages.Users
{
    public class SearchModel : PageModel
    {
        public List<SearchInfo> listSearch = new List<SearchInfo>();
        public SearchInfo searchInfo = new SearchInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String interest = Request.Query["interest"];
            try
            {
                String connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT interest FROM Interest";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SearchInfo searchInfo = new SearchInfo();
                                searchInfo.interest = reader.GetString(0);
                                listSearch.Add(searchInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            Response.Redirect("/Users/Preferences");
        }
    }

        public class SearchInfo
        {
            public String interest;
            public String name;
            public String biography;
            public String age;
        }

    }