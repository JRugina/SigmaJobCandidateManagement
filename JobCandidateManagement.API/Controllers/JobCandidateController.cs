using JobCandidateManagement.Business;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobCandidateManagement.Models;
using JobCandidateManagement.DataAccess.Entities;

namespace JobCandidateManagement.API.Controllers
{
    [Route("api/v1/jobcandidates")]
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

        /// <summary>
        /// Creates a new candidate or update an existing candidate
        /// </summary>
        /// <param name="emailAddress">The email address of the candidate</param>
        /// <param name="jobCandidateUpdateDTO">Candidate details</param>
        /// <response code="200">Creates or updates a candidate</response>
        /// <response code="400">If any of the required data is not pressent or is invalid.</response>
        /// <response code="401">If jwt token provided is invalid.</response>
        /// <returns>An IActionResult</returns>
        [HttpPut("{emailAddress}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateJobCandidate(string emailAddress,
            [FromBody] JobCandidateUpdateDTO jobCandidateUpdateDTO)
        {
            if (ModelState.IsValid)
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
            return BadRequest();
        }
    }
}