using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Protocols;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers
{   
    [Route("[controller]")]
    public class ApplicationsController : MainController
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;

        public ApplicationsController(
            IApplicationRepository applicationRepository,
            IApplicationService applicationService,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _applicationService = applicationService;
            _mapper = mapper;
        }

        // GET api/applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> Get()
        {
            return new ObjectResult(_mapper.Map<IEnumerable<ApplicationDto>>(await _applicationRepository.GetAllApplications()));
        }

        // GET api/applications/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDto>> Get(int id)
        {
            var app = _mapper.Map<ApplicationDto>(await _applicationRepository.GetApplication(id));
            if (app == null)
                return new NotFoundResult();

            return new ObjectResult(app);
        }

        // POST api/applications
        [HttpPost]
        public async Task<ActionResult<ApplicationDto>> Post([FromBody] ApplicationDto app)
        {
            app.ApplicationId = await _applicationRepository.GetNextId();
            var application = _mapper.Map<Application>(app);

            var result = await _applicationService.Post(application);

            if (!result) return BadRequest();

            return new OkObjectResult(app);
        }

        // PUT api/applications/1
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationDto>> Put(int id, [FromBody] Application app)
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
        public async Task<ActionResult<ApplicationDto>> Patch(int id, [FromBody] Application app)
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
