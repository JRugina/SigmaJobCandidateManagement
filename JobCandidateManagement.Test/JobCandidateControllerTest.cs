using AutoMapper;
using JobCandidateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using JobCandidateManagement.DataAccess.Services;
using JobCandidateManagement.API.Controllers;
using JobCandidateManagement.Business;
using System.Net;

namespace JobCandidateManagement.Test
{
    public class JobCandidateControllerTest
    {
        private readonly JobCandidateController _jobCandidateController;
        private readonly JobCandidateCSVService _jobCandidateService;

        public JobCandidateControllerTest()
        {
            var _jobCandidateCSVRepository = new JobCandidateCSVRepository();
            _jobCandidateService = new JobCandidateCSVService(_jobCandidateCSVRepository);

            var mapperConfiguration = new MapperConfiguration(
              cfg => cfg.AddProfile<JobCandidateManagement.Profiles.JobCandidateProfile>());
            var mapper = new Mapper(mapperConfiguration);

            _jobCandidateController = new JobCandidateController(_jobCandidateService, mapper);
        }

        [Fact]
        public void ShouldCreateANewCandidateAndReturnOkResult()
        {
            //Arrange
            var _jobCandidateDTO = new JobCandidateUpdateDTO()
            {
                FirstName = "Mike",
                LastName = "Jones",
                PhoneNumber = "100-202-123",
                EmailAddress = "mikejones@xyz.com",
                TimeInterval = new TimeSpan(2, 14, 18),
                LinkedInUrl = "https://www.linkedin.com/in/mike-jones",
                GitHubUrl = "https://www.github.com/MJones",
                Comments = "This candidate had applied for a job before."
            };

            //Act
            var response = _jobCandidateController.UpdateJobCandidate(_jobCandidateDTO.EmailAddress, _jobCandidateDTO);

            //Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void ShouldUpdateCandidateAndReturnOkResult()
        {
            //Arrange
            var _jobCandidateDTO = new JobCandidateUpdateDTO()
            {
                FirstName = "Mike",
                LastName = "Johnson",
                PhoneNumber = "100-202-123",
                EmailAddress = "mikejones@xyz.com",
                TimeInterval = new TimeSpan(2, 14, 18),
                LinkedInUrl = "https://www.linkedin.com/in/mike-johnson",
                GitHubUrl = "https://www.github.com/MJohnson",
                Comments = "This is a new applicant."
            };

            //Act
            var data = _jobCandidateController.UpdateJobCandidate(_jobCandidateDTO.EmailAddress, _jobCandidateDTO);

            //Assert  
            Assert.IsType<OkResult>(data);
        }


    }
}