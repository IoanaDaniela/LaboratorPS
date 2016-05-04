using System;
using System.Data.Entity;

namespace MVCTema2PS.Models
{
    public class User
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string usertype { get; set; }


    }


    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}