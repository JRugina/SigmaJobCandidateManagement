using JobCandidateManagement.DataAccess.Entities;

namespace JobCandidateManagement.DataAccess.Services
{
    public interface IJobCandidateRepository
    {
        void AddJobCandidate(JobCandidate jobCandidate);
        void UpdateJobCandidate(JobCandidate jobCandidate);
        JobCandidate? GetJobCandidate(string emailAddress);
        IEnumerable<JobCandidate> GetAllEmployees();
    }
}
