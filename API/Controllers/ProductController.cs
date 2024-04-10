using Core;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = WC.AdminRole)]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> objlist = _db.Product;
            foreach (var obj in objlist)
            {
                obj.Category =
                    _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
                obj.Type = _db.Type.FirstOrDefault(u => u.Id == obj.TypeId);
            }
            return Ok(objlist);
        }

        [HttpPost]
        public IActionResult Upsert(Product obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _db.Product.Add (obj);
                }
                else
                {
                    _db.Product.Update (obj);
                }
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        public IActionResult Create(Product obj)
        {
            _db.Product.Add (obj);
            _db.SaveChanges();
            return Ok(obj);
        }

        [HttpPut]
        public IActionResult Update(Product obj)
        {
            _db.Product.Update (obj);
            _db.SaveChanges();
            return Ok(obj);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product =
                _db
                    .Product
                    .Include(u => u.Category)
                    .Include(i => i.Type)
                    .FirstOrDefault(u => u.Id == id);
            _db.Product.Remove (product);
            _db.SaveChanges();
            return Ok(product);
        }
    }
}
