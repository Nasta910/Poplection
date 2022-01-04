using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poplection.Models
{
    class Set
    {
        [PrimaryKey, AutoIncrement]
        public int SetID { get; set; }

        [MaxLength(25)]
        public string SetName { get; set; }

        [MaxLength(500)]
        public string SetImage { get; set; }
    }
}
