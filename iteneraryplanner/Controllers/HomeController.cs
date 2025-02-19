using iteneraryplanner.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace iteneraryplanner.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration;

        public HomeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            List<MyData> customers = new List<MyData>();

            string sql = "SELECT * FROM Users";
            string constr = this.Configuration.GetConnectionString("DefaultConnection");

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new MyData
                            {
                                user_id = sdr["user_id"].ToString(),
                                name = sdr["name"].ToString(),
                                email = sdr["email"].ToString(),
                                password_hash = sdr["password_hash"].ToString(),
                                created_at = sdr["created_at"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }
            return View(customers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
