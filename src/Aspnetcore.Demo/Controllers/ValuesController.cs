using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspnetcore.Demo.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Aspnetcore.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController(IOptions<ApiOptions> options)
        {
            Options = options.Value;
        }

        private ApiOptions Options { get; }
        // GET api/values
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public ActionResult<IEnumerable<ApiOptions>> Get([FromServices] IOptionsSnapshot<ApiOptions> optionsSnapshot)
        {
            return new [] { optionsSnapshot.Value, Options };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ApiOptions> Get(int id, [FromServices] IOptionsSnapshot<ApiOptions> optionsSnapshot)
        {
            return optionsSnapshot.Value;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
