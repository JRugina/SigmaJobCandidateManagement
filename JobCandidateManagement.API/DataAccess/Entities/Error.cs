using Newtonsoft.Json;

namespace JobCandidateManagement.API.DataAccess.Entities
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
