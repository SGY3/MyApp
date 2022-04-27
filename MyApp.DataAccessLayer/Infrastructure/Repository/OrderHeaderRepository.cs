using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccessLayer.Infrastructure.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader orderHeader)
        {
            _context.OrderHeaders.Update(orderHeader);
            //var categoryDb=_context.Categories.FirstOrDefault(x=>x.Id==category.Id);
            //if (categoryDb != null)
            //{
            //    categoryDb.Name = category.Name;
            //    categoryDb.DisplayOrder = category.DisplayOrder;
            //} 
        }

        public void UpdateStatus(int Id, string orderStatus, string? paymentStaus = null)
        {
            var order = _context.OrderHeaders.FirstOrDefault(x => x.Id == Id);
            if (order != null)
            {
                order.OrderStatus = orderStatus;
            }
            if (paymentStaus != null)
            {
                order.PaymentStatus = paymentStaus;
            }
        }
    }
}
