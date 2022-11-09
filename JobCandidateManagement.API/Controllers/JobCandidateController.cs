using JobCandidateManagement.Business;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobCandidateManagement.Models;
using JobCandidateManagement.DataAccess.Entities;

namespace JobCandidateManagement.API.Controllers
{
    [Route("api/jobcandidates")]
    [ApiController]
    public class JobCandidateController : ControllerBase
    {
        private readonly IJobCandidateService _jobCandidateService;
        private readonly IMapper _mapper;

        public JobCandidateController(IJobCandidateService jobCandidateService,
            IMapper mapper)
        {
            _jobCandidateService = jobCandidateService;
            _mapper = mapper;
        }

        [HttpPut("{emailAddress}")]
        public IActionResult UpdateJobCandidate(string emailAddress,
            [FromBody] JobCandidateUpdateDTO jobCandidateUpdateDTO)
        {
            try
            {
                var jobCandidateFromRepo = _jobCandidateService.GetJobCandidate(emailAddress);

                if (jobCandidateFromRepo == null)
                {
                    var jobCandidateToAdd = _mapper.Map<JobCandidate>(jobCandidateUpdateDTO);

                    _jobCandidateService.AddJobCandidate(jobCandidateToAdd);

                    var jobCandidateToReturn = _mapper.Map<JobCandidateUpdateDTO>(jobCandidateToAdd);

                    return Ok();
                }

                _mapper.Map(jobCandidateUpdateDTO, jobCandidateFromRepo);

                _jobCandidateService.UpdateJobCandidate(jobCandidateFromRepo);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
