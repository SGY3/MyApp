using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [ValidateNever]
        public Product Product { get; set; }
        [ValidateNever]
        public string AppliationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser AppliationUser { get; set; }
        public int Count { get; set; }
    }
}
