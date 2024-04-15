using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Pages.Users;
using System.Data.SqlClient;

namespace LoveSync_DBProject.Pages.Users
{
    public class PreferencesModel : PageModel
    {
        public List<PreferenceInfo> listPreference { get; set; }
        public String errorMessage { get; set; }

        public void OnGet(string interest)
        {
            listPreference = new List<PreferenceInfo>();

            try
            {
                String connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT name, biography, age, interest FROM Users_Table WHERE interest=@interest";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@interest", interest);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PreferenceInfo preferenceInfo = new PreferenceInfo();
                                preferenceInfo.name = reader.GetString(0);
                                preferenceInfo.biography = reader.GetString(1);
                                preferenceInfo.age = reader.GetInt32(2).ToString();
                                preferenceInfo.interest = reader.GetString(3);

                                listPreference.Add(preferenceInfo);
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
            Response.Redirect("/Users/Matches");
        }
    }

    public class PreferenceInfo
    {
        public String name { get; set; }
        public String biography { get; set; }
        public String age { get; set; }
        public String interest { get; set; }
    }
}