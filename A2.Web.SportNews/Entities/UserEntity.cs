using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Web.SportNews.Entities
{
    public class UserEntity : Entity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
