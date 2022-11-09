using AutoMapper;

namespace JobCandidateManagement.Profiles
{
    public class JobCandidateProfile : Profile
    {
        public JobCandidateProfile()
        {
            CreateMap<Models.JobCandidateCreationDTO, DataAccess.Entities.JobCandidate>();
            CreateMap<Models.JobCandidateUpdateDTO, DataAccess.Entities.JobCandidate>();
            CreateMap<DataAccess.Entities.JobCandidate, Models.JobCandidateUpdateDTO>();
        }
    }
}
