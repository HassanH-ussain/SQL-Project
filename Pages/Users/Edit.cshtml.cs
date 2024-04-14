using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;

namespace LoveSync.Pages.Users
{
    public class EditModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userInfo.id = "" + reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.phone = reader.GetString(3);
                                userInfo.address = reader.GetString(4);
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
            userInfo.id = Request.Form["id"];
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.address = Request.Form["address"];

            if(userInfo.id.Length == 0 || userInfo.name.Length == 0 ||
               userInfo.email.Length == 0 || userInfo.phone.Length == 0 ||
               userInfo.address.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE users " +
                        "SET name=@name, email=@email, phone=@phone, address=@address " +
                        "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@address", userInfo.address);
                        command.Parameters.AddWithValue("@id", userInfo.id);

                        command.ExecuteNonQuery();

                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Users/Index");
        }
    }
}



//String id = Request.Query["id"];

//string connectionString = "Data Source = SHAWTY; Initial Catalog = dating_project; Integrated Security = True; Encrypt = True; TrustServerCertificate = True";
//using (SqlConnection connection = new SqlConnection(connectionString))
//{
//    connection.Open();

//    String sql = "DELETE FROM users WHERE id=@id";
//    using (SqlCommand command = new SqlCommand(sql, connection))
//    {
//        command.Parameters.AddWithValue("@id", id);

//        command.ExecuteNonQuery();
//    }
//}
//}
//catch(Exception ex){

//}
//Response.Redirect("/Users/Index");