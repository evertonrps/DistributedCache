using DistributedCache.Domain;
using DistributedCache.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace DistributedCache.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IDistributedCacheService distributedCacheService) : ControllerBase
    {
        private readonly IDistributedCacheService _distributedCacheService = distributedCacheService;

        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _distributedCacheService.GetData<Product>(id);
            if (result == null)
            {
                return NotFound("not found");
            }
            return Ok(result);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post(string key, string value)
        {
            try
            {
                var result = await _distributedCacheService.SetData(key, value);
                if (result)
                {
                    return Ok("success");
                }

                return BadRequest("fail");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _distributedCacheService.RemoveData(id))
            {
                return Ok("success");
            }

            return NotFound("error");
        }
    }
}
