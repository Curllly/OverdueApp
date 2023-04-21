using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace OverdueMobileDesktop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public string Title { get; set; }
        public DateTime Manufactured { get; set; }
        public DateTime BestBefore { get; set; }
        public int Count { get; set; }
    }
}
