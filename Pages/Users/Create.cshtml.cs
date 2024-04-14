using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyStore.Pages.Users;
using System.Data.SqlClient;

namespace LoveSync_DBProject.Pages.Users
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.address = Request.Form["address"];
            userInfo.biography = Request.Form["biography"];
            userInfo.age = Request.Form["age"];
            userInfo.interest = Request.Form["interest"];

            if (userInfo.name.Length == 0 || userInfo.email.Length == 0 ||
                userInfo.phone.Length == 0 || userInfo.address.Length == 0 ||
                userInfo.biography.Length == 0 || userInfo.age.Length == 0 ||
                userInfo.interest.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client into the database
            try
            {
                String connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Users_Table " +
                                 "(name, email, phone, address, biography, age, interest) VALUES " +
                                 "(@name, @email, @phone, @address, @biography, @age, @interest);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@address", userInfo.address);
                        command.Parameters.AddWithValue("@biography", userInfo.biography);
                        command.Parameters.AddWithValue("@age", userInfo.age);
                        command.Parameters.AddWithValue("@interest", userInfo.interest);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            userInfo.name = ""; userInfo.email = ""; userInfo.phone = ""; userInfo.address = ""; userInfo.biography = ""; userInfo.age = ""; userInfo.interest = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Users/Index");
        }
    }
}
