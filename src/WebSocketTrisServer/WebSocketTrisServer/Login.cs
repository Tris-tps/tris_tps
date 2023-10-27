using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketTrisServer.Data;
using WebSocketTrisServer.Model;

namespace WebSocketTrisServer
{
    public class Login
    {
        
        private readonly LoginDbContext _dbContext;

        public Login()
        {
            _dbContext = new LoginDbContext();
        }

        public bool RegisterUser(string username)
        {
            if (_dbContext.Users.Any(u => u.Username == username))
            {
                return false; // L'utente esiste già
            }

            var user = new User { Username = username };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return true; // Registrazione avvenuta con successo
        }

        public bool AuthenticateUser(string username)
        {
            return _dbContext.Users.Any(u => u.Username == username );
        }
    }
}
