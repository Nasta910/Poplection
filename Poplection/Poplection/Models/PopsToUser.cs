using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poplection.Models
{
    public class PopsToUser
    {
        [PrimaryKey, AutoIncrement]
        public int PopToCollectionID { get; set; }

        public int PopID { get; set; }

        public int UserID { get; set; }
        
        public int StoreID { get; set; }

        public float PopPrice { get; set; }

        public float ShippingPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public bool PopDelivered { get; set; }

        public bool PopProtected { get; set; }

    }
}
