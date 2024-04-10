using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using Core;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
     [Authorize(Roles = WC.AdminRole)]
    public class CategoryController:ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> objlist = _db.Category;
            return Ok(objlist);

        }
        [HttpPost]
         public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid){
                _db.Category.Add(obj);
                _db.SaveChanges();
                return Ok(obj);
            }
            
            return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Edit(Category obj)
        {
            if(ModelState.IsValid){
                _db.Category.Update(obj);
                _db.SaveChanges();
                return Ok(obj);
            }
            
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _db.Category.Find(id);
            if(obj == null){
                return NotFound();
            }
            _db.Category.Remove(obj);
            _db.SaveChanges();
            return Ok(obj);
        }
    }
}