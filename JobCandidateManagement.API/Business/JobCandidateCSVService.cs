using JobCandidateManagement.DataAccess.Entities;
using JobCandidateManagement.DataAccess.Services;

namespace JobCandidateManagement.Business
{
    public class JobCandidateCSVService:IJobCandidateService
    {
        private readonly IJobCandidateRepository _repository;

        public JobCandidateCSVService(IJobCandidateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void AddJobCandidate(JobCandidate jobCandidate)
        {
            _repository.AddJobCandidate(jobCandidate);
        }
        public void UpdateJobCandidate(JobCandidate jobCandidate)
        {
            _repository.UpdateJobCandidate(jobCandidate);
        }
        public JobCandidate? GetJobCandidate(string emailAddress)
        {
            return  _repository.GetJobCandidate(emailAddress);
        }
        public IEnumerable<JobCandidate> GetAllEmployees()
        {
            return _repository.GetAllEmployees();
        }
    }
}
