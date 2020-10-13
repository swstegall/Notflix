using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Notflix
{
    public class User
    {
        public Boolean isLoggedIn { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public double money { get; set; }
        public int movies { get; set; }

        public User (string user, string password)
        {
            this.isLoggedIn = true;
            this.username = user;
            this.password = password;
            this.money = 5000.0;
            this.movies = 0;
        }

        public void purchase (double amount)
        {
            this.money -= amount;
            ++this.movies;
        }
    }

    public class Program
    {
        public static User u = new User("sam", "password");

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
