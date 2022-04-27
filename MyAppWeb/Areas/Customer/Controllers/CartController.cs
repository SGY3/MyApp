using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.DataAccessLayer.Infrastructure.IRepository;
using MyApp.Models;
using MyApp.Models.ViewModels;
using System.Security.Claims;

namespace MyAppWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartVM itemList { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //to get Application id of user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            CartVM itemList = new CartVM()
            {
                ListOfCart = _unitOfWork.Cart.GetAll(x => x.AppliationUserId == claims.Value, includeProperties: "Product")
            };
            foreach (var item in itemList.ListOfCart)
            {
                itemList.Total += (item.Product.Price * item.Count);
            }
            return View(itemList);
        }
        public IActionResult plus(int id)
        {
            //to get Application id of user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var cart = _unitOfWork.Cart.GetT(x => x.Id == id && x.AppliationUserId == claims.Value);
            _unitOfWork.Cart.IncrementCartItem(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int id)
        {
            //to get Application id of user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var cart = _unitOfWork.Cart.GetT(x => x.Id == id && x.AppliationUserId == claims.Value);
            if (cart.Count <= 1)
            {
                _unitOfWork.Cart.Delete(cart);
            }
            else
            {
                _unitOfWork.Cart.DecrementCartItem(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult delete(int id)
        {
            //to get Application id of user
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var cart = _unitOfWork.Cart.GetT(x => x.Id == id && x.AppliationUserId == claims.Value);

            _unitOfWork.Cart.Delete(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
