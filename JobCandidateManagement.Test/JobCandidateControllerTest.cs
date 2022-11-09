using AutoMapper;
using JobCandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using JobCandidateManagement.DataAccess.Services;
using JobCandidateManagement.API.Controllers;
using JobCandidateManagement.Business;

namespace JobCandidateManagement.Test
{
    public class JobCandidateControllerTest
    {
        private readonly JobCandidateController _jobCandidateController;

        public JobCandidateControllerTest()
        {
            var jobCandidateCSVRepository = new JobCandidateCSVRepository();
            var jobCandidateService = new JobCandidateCSVService(jobCandidateCSVRepository);

            var mapperConfiguration = new MapperConfiguration(
              cfg => cfg.AddProfile<JobCandidateManagement.Profiles.JobCandidateProfile>());
            var mapper = new Mapper(mapperConfiguration);

            _jobCandidateController = new JobCandidateController(jobCandidateService, mapper);
        }
    }
}