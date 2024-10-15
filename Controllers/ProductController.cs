using Microsoft.AspNetCore.Mvc;
using WebApp_vSem1.Data;
using WebApp_vSem1.Models;

namespace WebApp_vSem1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost("add_product")]
        public ActionResult<int> AddProduct(string name, int groupId, string description, decimal price)
        {
            using (WebAppContext ctx = new WebAppContext())
            {
                if (ctx.Products.Any(p => p.Name == name))
                    return StatusCode(409);

                var product = new Product() { Name = name, ProductGroupId = groupId, Description = description, Price = price };
                ctx.Products.Add(product);
                ctx.SaveChanges();
                return Ok($"Получен Id = {product.Id}");
            }
        }

        [HttpGet("get_products")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            IEnumerable<Product> listProducts;
            using (WebAppContext ctx = new WebAppContext())
            {
                listProducts = ctx.Products.Select(p => new Product { Name = p.Name, Description = p.Description, Price = p.Price, ProductGroupId = p.ProductGroupId}).ToList();
                return Ok(listProducts);
            }
        }

        [HttpDelete("delete_product_on_id")]
        public ActionResult DeleteProduct(int id)
        {
            using (WebAppContext ctx = new WebAppContext())
            {
                var product = ctx.Products.Find(id);
                if (product == null)
                    return NotFound();

                ctx.Products.Remove(product);
                ctx.SaveChanges();
                return NoContent();
            }
        }       

        [HttpPut("set_price_on_id")]
        public ActionResult UpdateProductPrice(int id, decimal newPrice)
        {
            using (WebAppContext ctx = new WebAppContext())
            {
                var prod = ctx.Products.Find(id);
                if (prod == null)
                    return NotFound();

                var oldPrice = prod.Price;
                prod.Price = newPrice;
                ctx.SaveChanges();
                return Ok($"Старая цена = {oldPrice}");
            }
        }
    }
}
