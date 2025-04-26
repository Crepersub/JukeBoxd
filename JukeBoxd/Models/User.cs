using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JukeBoxd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<Entry> Entries { get; } = new();
        public User(string username) 
        {
            Username = username;
        }
    }
}
