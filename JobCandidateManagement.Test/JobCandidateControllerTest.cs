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

            var result = _jobCandidateService.GetJobCandidate(_jobCandidateDTO.EmailAddress);
            Assert.NotNull(result);
            if (result != null)
            {
                Assert.Equal(_jobCandidateDTO.FirstName, result.FirstName);
                Assert.Equal(_jobCandidateDTO.LastName, result.LastName);
                Assert.Equal(_jobCandidateDTO.PhoneNumber, result.PhoneNumber);
                Assert.Equal(_jobCandidateDTO.EmailAddress, result.EmailAddress);
                Assert.Equal(_jobCandidateDTO.TimeInterval, result.TimeInterval);
                Assert.Equal(_jobCandidateDTO.LinkedInUrl, result.LinkedInUrl);
                Assert.Equal(_jobCandidateDTO.GitHubUrl, result.GitHubUrl);
                Assert.Equal(_jobCandidateDTO.Comments, result.Comments);
            }
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

            var result = _jobCandidateService.GetJobCandidate(_jobCandidateDTO.EmailAddress);
            Assert.NotNull(result);
            if (result != null)
            {
                Assert.Equal(_jobCandidateDTO.FirstName, result.FirstName);
                Assert.Equal(_jobCandidateDTO.LastName, result.LastName);
                Assert.Equal(_jobCandidateDTO.PhoneNumber, result.PhoneNumber);
                Assert.Equal(_jobCandidateDTO.EmailAddress, result.EmailAddress);
                Assert.Equal(_jobCandidateDTO.TimeInterval, result.TimeInterval);
                Assert.Equal(_jobCandidateDTO.LinkedInUrl, result.LinkedInUrl);
                Assert.Equal(_jobCandidateDTO.GitHubUrl, result.GitHubUrl);
                Assert.Equal(_jobCandidateDTO.Comments, result.Comments);
            }
        }

        [Fact]
        public void ShouldReturnBadRequest_InvalidEmail()
        {
            //Arrange
            _jobCandidateController.ModelState.Clear();
            _jobCandidateController.ModelState.AddModelError("EmailAddress", "Invalid email address");

            var _jobCandidateUpdateDTO = new JobCandidateUpdateDTO()
            {
                FirstName = "Sam",
                LastName = "Andrew",
                PhoneNumber = "100-202-123",
                EmailAddress = "samandrew.xyz.com",
                TimeInterval = new TimeSpan(2, 14, 18),
                LinkedInUrl = "https://www.linkedin.com/in/mike-jones",
                GitHubUrl = "https://www.github.com/MJones",
                Comments = "New job applicant."
            };

            //Act
            var result = _jobCandidateController.UpdateJobCandidate(_jobCandidateUpdateDTO.EmailAddress,
                _jobCandidateUpdateDTO);

            //Assert
            Assert.IsType<BadRequestResult>(result);

            //Make sure Name has correct errors
            Assert.False(_jobCandidateController.ModelState.IsValid);
            Assert.True(_jobCandidateController.ModelState.ErrorCount > 0);
            Assert.True(_jobCandidateController.ModelState["EmailAddress"].Errors.Count > 0);
            Assert.Equal(_jobCandidateController.ModelState["EmailAddress"].Errors[0].ErrorMessage, "Invalid email address");
        }

        [Fact]
        public void ShouldReturnBadRequest_MissingRequiredFields()
        {
            //Arrange
            _jobCandidateController.ModelState.Clear();
            _jobCandidateController.ModelState.AddModelError("FirstName", "The first name is required");
            _jobCandidateController.ModelState.AddModelError("LastName", "The last name is required");
            _jobCandidateController.ModelState.AddModelError("EmailAddress", "The email address is required");
            _jobCandidateController.ModelState.AddModelError("Comments", "Comments are required");

            var _jobCandidateUpdateDTO = new JobCandidateUpdateDTO()
            {
                PhoneNumber = "100-202-123",
                TimeInterval = new TimeSpan(2, 14, 18),
                LinkedInUrl = "https://www.linkedin.com/in/mike-jones",
                GitHubUrl = "https://www.github.com/MJones"
            };

            //Act
            var result = _jobCandidateController.UpdateJobCandidate(_jobCandidateUpdateDTO.EmailAddress,
                _jobCandidateUpdateDTO);

            //Assert
            Assert.IsType<BadRequestResult>(result);

            //Make sure Name has correct errors
            Assert.False(_jobCandidateController.ModelState.IsValid);
            Assert.True(_jobCandidateController.ModelState.ErrorCount > 0);
            Assert.Equal(4, _jobCandidateController.ModelState.ErrorCount);
        }
    }
}