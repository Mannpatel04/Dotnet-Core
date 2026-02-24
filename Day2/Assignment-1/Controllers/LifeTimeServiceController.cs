using Assignment_1.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_1.Controllers
{
    [Route("api/lifetime")]
    [ApiController]
    public class LifeTimeServiceController : ControllerBase
    {

        private readonly ITransientService _transient;
        private readonly IScopedService _scoped;
        private readonly ISingletonService _singleton;

        public LifeTimeServiceController(
          ITransientService transient,
          IScopedService scoped,
          ISingletonService singleton)
        {
            _transient = transient;
            _scoped = scoped;
            _singleton = singleton;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Transient = _transient.Id,
                Scoped = _scoped.Id,
                Singleton = _singleton.Id
            });
        }

    }
}
