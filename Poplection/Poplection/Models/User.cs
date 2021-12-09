using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poplection.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        [MaxLength(25)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string ProfileImage { get; set; }

    }
}
