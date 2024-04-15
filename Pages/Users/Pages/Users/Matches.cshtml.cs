using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LoveSync_DBProject.Pages.Users
{
    public class MatchesModel : PageModel
    {
        public List<MatchesInfo> listMatches { get; set; }
        public string errorMessage { get; set; }

        public MatchesModel()
        {
            listMatches = new List<MatchesInfo>(); // Initialize listMatches in the constructor
        }

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT match_id, matcher, matched_with, match_date FROM Matches";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MatchesInfo matchesInfo = new MatchesInfo();
                                matchesInfo.id = reader.GetInt32(0).ToString();
                                matchesInfo.matcher = reader.GetString(1);
                                matchesInfo.matched_with = reader.GetString(2);
                                matchesInfo.matched_date = reader.GetDateTime(3).ToString();
                                listMatches.Add(matchesInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Exception: " + ex.ToString();
            }
        }
    }

    public class MatchesInfo
    {
        public string id { get; set; }
        public string matcher { get; set; }
        public string matched_with { get; set; }
        public string matched_date { get; set; }
    }
}
