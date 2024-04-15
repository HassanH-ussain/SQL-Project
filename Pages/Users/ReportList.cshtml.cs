using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LoveSync_DBProject.Pages.Users
{
    public class ReportListModel : PageModel
    {
        public List<ReportInfo> listReports = new List<ReportInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=SHAWTY;Initial Catalog=dating_project;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Report_Table";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReportInfo reportInfo = new ReportInfo();
                                reportInfo.ReportID = reader.GetInt32(0).ToString();
                                reportInfo.ReportedUserID = reader.GetString(1);
                                reportInfo.ReporterID = reader.GetString(2);
                                reportInfo.ReportReason = reader.GetString(3);
                                reportInfo.ReportDate = reader.GetDateTime(4).ToString();

                                listReports.Add(reportInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ReportInfo
    {
        public string ReportID { get; set; }
        public string ReportedUserID { get; set; }
        public string ReporterID { get; set; }
        public string ReportReason { get; set; }
        public string ReportDate { get; set; }
    }
}
