using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace LoveSync.Pages.Users
{
    public class CreateModel : PageModel
    {
        public UserInfo userInfo = new UserInfo();

        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() {
            userInfo.name = Request.Form["name"];
            userInfo.email = Request.Form["email"];
            userInfo.phone = Request.Form["phone"];
            userInfo.address = Request.Form["address"];

            if (userInfo.name.Length == 0 || userInfo.email.Length == 0 ||
                userInfo.phone.Length == 0 || userInfo.address.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            //save the new client into the database
            try
            {
                String connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users " +
                        "(name, email, phone, address) VALUES " +
                        "(@name, @email, @phone, @address);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userInfo.name);
                        command.Parameters.AddWithValue("@email", userInfo.email);
                        command.Parameters.AddWithValue("@phone", userInfo.phone);
                        command.Parameters.AddWithValue("@address", userInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            userInfo.name = ""; userInfo.email = ""; userInfo.phone = ""; userInfo.address = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Users/Index");
        }

    }
}
