using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poplection.Models
{
    public class Pop
    {
        [PrimaryKey, AutoIncrement]
        public int PopID { get; set; }

        [MaxLength(25)]
        public string PopName { get; set; }

        [MaxLength(4)]
        public int PopNumber { get; set; }

        [MaxLength(4)]
        public int SetID { get; set; }

        [MaxLength(500)]
        public string PopImage { get; set; }
    }
}
