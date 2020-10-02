using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public int Number { get; set; }

        public decimal TotalPrice { get; set; }

        public User User { get; set; }
    }
}
