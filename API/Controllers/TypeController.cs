using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Type = Core.Entities.Type;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = WC.AdminRole)]
    public class TypeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public TypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Type> objlist = _db.Type;
            return Ok(objlist);

        }
        [HttpPost]
         public IActionResult Create(Type obj)
        {
            
             _db.Type.Add(obj);
             _db.SaveChanges();
             return Ok(obj);

        }
        [HttpPut]
        public IActionResult Edit(Type obj)
        {
            
             _db.Type.Update(obj);
             _db.SaveChanges();
             return Ok(obj);

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _db.Type.Find(id);
            if(obj == null){
                return NotFound();
            }
            _db.Type.Remove(obj);
            _db.SaveChanges();
            return Ok(obj);
        }
    }
   
}