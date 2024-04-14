using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace LoveSync.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> listUsers = new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.phone = reader.GetString(3);
                                userInfo.address = reader.GetString(4);
                                userInfo.biography = reader.GetString(5);
                                userInfo.interest = reader.GetString(6);
                                userInfo.age = "" + reader.GetInt32(7);
                                userInfo.created_at = reader.GetDateTime(8).ToString();

                                listUsers.Add(userInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }



    public class UserInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
        public String biography;
        public String age;
        public String interest;
    }
}
