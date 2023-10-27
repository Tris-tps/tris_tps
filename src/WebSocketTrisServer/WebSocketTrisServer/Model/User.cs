using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketTrisServer.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public  string? Password { get; set; }
    }
}
