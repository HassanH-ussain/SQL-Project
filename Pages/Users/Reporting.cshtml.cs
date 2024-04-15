using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace LoveSync_DBProject.Pages.Users
{
    public class ReportingModel : PageModel
    {
        public ReportInfo reportInfo = new ReportInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            // Automatically generate the next ReportID
            reportInfo.ReportID = GetNextReportID().ToString();
        }

        public void OnPost()
        {
            reportInfo.ReportID = GetNextReportID().ToString();
            reportInfo.ReportedUserID = Request.Form["ReportedUserID"];
            reportInfo.ReporterID = Request.Form["ReporterID"];
            reportInfo.ReportReason = Request.Form["ReportReason"];
            reportInfo.ReportDate = Request.Form["ReportDate"];

            if (string.IsNullOrEmpty(reportInfo.ReportedUserID) ||
                string.IsNullOrEmpty(reportInfo.ReporterID) || string.IsNullOrEmpty(reportInfo.ReportReason) ||
                string.IsNullOrEmpty(reportInfo.ReportDate))
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Report_Table (ReportID, ReporterID, ReportedUserID, ReportReason, ReportDate) " +
                                 "VALUES (@ReportID, @ReporterID, @ReportedUserID, @ReportReason, @ReportDate);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReportID", reportInfo.ReportID);
                        command.Parameters.AddWithValue("@ReporterID", reportInfo.ReporterID);
                        command.Parameters.AddWithValue("@ReportedUserID", reportInfo.ReportedUserID);
                        command.Parameters.AddWithValue("@ReportReason", reportInfo.ReportReason);
                        command.Parameters.AddWithValue("@ReportDate", reportInfo.ReportDate);

                        command.ExecuteNonQuery();
                    }
                }
                successMessage = "Report added successfully. We will investigate as soon as possible.";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Response.Redirect("/Users/ReportList");
        }

        private int GetNextReportID()
        {
            int nextReportID = 0;
            try
            {
                string connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT ISNULL(MAX(ReportID), 0) + 1 FROM Report_Table;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        nextReportID = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return nextReportID;
        }
    }
}
