using JobCandidateManagement.DataAccess.Entities;

namespace JobCandidateManagement.Business
{
    public interface IJobCandidateService
    {
        void AddJobCandidate(JobCandidate jobCandidate);
        void UpdateJobCandidate(JobCandidate jobCandidate);
        JobCandidate? GetJobCandidate(string emailAddress);
    }
}
