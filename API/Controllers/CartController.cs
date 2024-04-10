using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using API.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
  
    public class CartController : ControllerBase
    {
           private  ApplicationDbContext _db;
           private readonly IWebHostEnvironment _hostingEnvironment;
           private readonly IEmailSender _emailSender;
           
        public CartController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment, IEmailSender emailSender)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            _emailSender = emailSender;
        }
       
        
        [HttpGet]
        public IActionResult Index()
        {
            List<ShopingCart> shopigCartList = new List<ShopingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard)!=null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard).Count()>0)
            {
                shopigCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCard);

            }
            List<int> prodInCart = shopigCartList.Select(i=>i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u=>prodInCart.Contains(u.Id));
            return Ok(shopigCartList);
        }
        [HttpPost]
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            List<ShopingCart> shopigCartList = new List<ShopingCart>();
           
            if(HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard)!=null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard).Count()>0)
            {
                shopigCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCard);

            }
            List<int> prodInCart = shopigCartList.Select(i=>i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u=>prodInCart.Contains(u.Id));
            return Ok(shopigCartList);
        }

        [HttpPost]
        [Route("Remove")]
        public IActionResult Remove(int id)
        {
            List<ShopingCart> shopigCartList = new List<ShopingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard)!=null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard).Count()>0)
            {
                shopigCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCard);

            }
            shopigCartList.Remove(shopigCartList.FirstOrDefault(u=>u.ProductId==id));
            HttpContext.Session.Set(WC.SessionCard,shopigCartList);
            return Ok(shopigCartList);
        }
        [HttpPost]
        [Route("Plus")]
        public IActionResult Plus(int cartId)
        {
            List<ShopingCart> shopigCartList = new List<ShopingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard)!=null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard).Count()>0)
            {
                shopigCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCard);

            }
            shopigCartList.FirstOrDefault(u=>u.Id==cartId).Count += 1;
            HttpContext.Session.Set(WC.SessionCard,shopigCartList);
            return Ok(shopigCartList);
        }
        [HttpPost]
        [Route("Minus")]
        public IActionResult Minus(int cartId)
        {
            List<ShopingCart> shopigCartList = new List<ShopingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard)!=null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCard).Count()>0)
            {
                shopigCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCard);

            }
            if(shopigCartList.FirstOrDefault(u=>u.Id==cartId).Count == 1)
            {
                var itemToRemove = shopigCartList.SingleOrDefault(r => r.Id == cartId);
                if (itemToRemove != null)
                    shopigCartList.Remove(itemToRemove);
                HttpContext.Session.Set(WC.SessionCard,shopigCartList);
                return Ok(shopigCartList);
            }
            else
            {
                shopigCartList.FirstOrDefault(u=>u.Id==cartId).Count -= 1;
                HttpContext.Session.Set(WC.SessionCard,shopigCartList);
                return Ok(shopigCartList);
            }
        }

    }
}