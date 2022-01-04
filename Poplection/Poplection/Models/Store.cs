using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poplection.Models
{
    class Store
    {
        [PrimaryKey, AutoIncrement]
        public int StoreID { get; set; }

        [MaxLength(25)]
        public string StoreName { get; set; }

        [MaxLength(500)]
        public string StoreURL { get; set; }

        [MaxLength(500)]
        public string StoreImage { get; set; }
    }
}
