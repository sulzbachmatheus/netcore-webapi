using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationRepository _repo;

        public ApplicationsController(IApplicationRepository repo)
        {
            _repo = repo;
        }

        // GET api/applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> Get()
        {
            return new ObjectResult(await _repo.GetAllApplications());
        }

        // GET api/applications/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> Get(int id)
        {
            var app = await _repo.GetApplication(id);
            if (app == null)
                return new NotFoundResult();

            return new ObjectResult(app);
        }

        // POST api/applications
        [HttpPost]
        public async Task<ActionResult<Application>> Post([FromBody] Application app)
        {
            app.ApplicationId = await _repo.GetNextId();
            await _repo.Create(app);
            return new OkObjectResult(app);
        }

        // PUT api/applications/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> Put(int id, [FromBody] Application app)
        {
            var appFromDb = await _repo.GetApplication(id);
            if (appFromDb == null)
                return new NotFoundResult();

            app.ApplicationId = appFromDb.ApplicationId;
            app.InternalId = appFromDb.InternalId;
            await _repo.Update(app);
            return new OkObjectResult(app);
        }

        // PATCH api/applications/1
        [HttpPatch("{id}")]
        public async Task<ActionResult<Application>> Patch(int id, [FromBody] Application app)
        {
            var appFromDb = await _repo.GetApplication(id);
            if (appFromDb == null)
                return new NotFoundResult();

            app.ApplicationId = appFromDb.ApplicationId;
            app.InternalId = appFromDb.InternalId;
            await _repo.PartialUpdate(app);
            return new OkObjectResult(app);
        }

        // DELETE api/applications/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _repo.GetApplication(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }

    }
}
