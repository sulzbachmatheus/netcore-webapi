using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {

    }

    [Route("[controller]")]
    public class ApplicationsController : MainController
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationsController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        // GET api/applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> Get()
        {
            return new ObjectResult(await _applicationRepository.GetAllApplications());
        }

        // GET api/applications/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> Get(int id)
        {
            var app = await _applicationRepository.GetApplication(id);
            if (app == null)
                return new NotFoundResult();

            return new ObjectResult(app);
        }

        // POST api/applications
        [HttpPost]
        public async Task<ActionResult<Application>> Post([FromBody] Application app)
        {
            app.ApplicationId = await _applicationRepository.GetNextId();
            await _applicationRepository.Create(app);

            return new OkObjectResult(app);
        }

        // PUT api/applications/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Application>> Put(int id, [FromBody] Application app)
        {
            var appFromDb = await _applicationRepository.GetApplication(id);
            if (appFromDb == null)
                return new NotFoundResult();

            app.ApplicationId = appFromDb.ApplicationId;
            app.InternalId = appFromDb.InternalId;
            await _applicationRepository.Update(app);
            return new OkObjectResult(app);
        }

        // PATCH api/applications/1
        [HttpPatch("{id}")]
        public async Task<ActionResult<Application>> Patch(int id, [FromBody] Application app)
        {
            var appFromDb = await _applicationRepository.GetApplication(id);
            if (appFromDb == null)
                return new NotFoundResult();

            app.ApplicationId = appFromDb.ApplicationId;
            app.InternalId = appFromDb.InternalId;
            await _applicationRepository.PartialUpdate(app);
            return new OkObjectResult(app);
        }

        // DELETE api/applications/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _applicationRepository.GetApplication(id);
            if (post == null)
                return new NotFoundResult();

            await _applicationRepository.Delete(id);
            return new OkResult();
        }

    }
}
