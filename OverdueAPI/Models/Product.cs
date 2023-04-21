using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace OverdueAPI.Models
{
    public class Product : BaseModel
    {
        public string Title { get; set; }
        public DateTime Manufactured { get; set; }
        public DateTime BestBefore { get; set; }
        public int Count { get; set; }
        public int PlaceId { get; set; }
        [JsonIgnore]
        public virtual Place? Place { get; set; }
    }
}
