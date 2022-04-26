using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public string AppliationUserId { get; set; }
        public ApplicationUser AppliationUser { get; set; }
        public int Count { get; set; }
    }
}
