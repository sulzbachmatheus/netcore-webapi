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
            IMapper mapper,
            INotifier notifier) : base(notifier)
        {
            _applicationRepository = applicationRepository;
            _applicationService = applicationService;
            _mapper = mapper;
        }

        // GET api/applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> Get()
        {
            var result = await _applicationRepository.GetAllApplications();
            var response = new ObjectResult(_mapper.Map<IEnumerable<ApplicationDto>>(result));
            return response;
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
        public async Task<ActionResult<ApplicationDto>> Post([FromBody] ApplicationDto appDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            appDto.ApplicationId = await _applicationRepository.GetNextId();

            await _applicationService.Post(_mapper.Map<Application>(appDto));
            return CustomResponse(appDto);
        }

        // PUT api/applications/1
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationDto>> Put(int id, [FromBody] ApplicationDto appDto)
        {
            if (id != appDto.ApplicationId) return BadRequest();
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var appFromDb = await _applicationRepository.GetApplication(id);
            if (appFromDb == null)
                return new NotFoundResult();

            appDto.ApplicationId = appFromDb.ApplicationId;
            await _applicationService.Put(_mapper.Map<Application>(appDto), appFromDb.InternalId);
                        
            return CustomResponse(appDto);
        }

        // PATCH api/applications/1
        [HttpPatch("{id}")]
        public async Task<ActionResult<ApplicationDto>> Patch(int id, [FromBody] ApplicationDto appDto)
        {
            if (id != appDto.ApplicationId) return BadRequest();
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var appFromDb = await _applicationRepository.GetApplication(id);
            if (appFromDb == null)
                return new NotFoundResult();

            appDto.ApplicationId = appFromDb.ApplicationId;
            await _applicationService.Patch(_mapper.Map<Application>(appDto), appFromDb.InternalId);

            return CustomResponse(appDto);
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
