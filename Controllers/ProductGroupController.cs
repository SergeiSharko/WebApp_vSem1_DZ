using Microsoft.AspNetCore.Mvc;
using WebApp_vSem1.Data;
using WebApp_vSem1.Models;

namespace WebApp_vSem1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {
        [HttpPost("add_group")]
        public ActionResult AddGroup(string name, string description)
        {
            using (WebAppContext ctx = new WebAppContext())
            {
                if (ctx.ProductGroups.Any(pg => pg.Name!.ToLower() == name.ToLower()))
                    return StatusCode(409);

                var productGroup = new ProductGroup() { Name = name, Description = description };
                ctx.ProductGroups.Add(productGroup);
                ctx.SaveChanges();

                return Ok($"Получен Id = {productGroup.Id}");
            }
        }

        [HttpGet("get_groups")]
        public ActionResult<IEnumerable<ProductGroup>> GetAllGroups()
        {
            IEnumerable<ProductGroup> listGroups;
            using (WebAppContext ctx = new WebAppContext())
            {
                listGroups = ctx.ProductGroups.Select(p => new ProductGroup { Name = p.Name, Description = p.Description }).ToList();
                return Ok(listGroups);
            }
        }

        [HttpDelete("delete_group_on_id")]
        public ActionResult DeleteGroup(int id)
        {
            using (WebAppContext ctx = new WebAppContext())
            {
                var group = ctx.ProductGroups.Find(id);
                if (group == null)
                    return NotFound();

                ctx.ProductGroups.Remove(group);
                ctx.SaveChanges();
                return NoContent();
            }
        }
    }
}
