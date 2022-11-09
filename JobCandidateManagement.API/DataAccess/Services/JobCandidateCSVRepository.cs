using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Timers;
using System.Text;
using System.IO;
using JobCandidateManagement.DataAccess.Entities;
using System.Reflection;

namespace JobCandidateManagement.DataAccess.Services
{
    public class JobCandidateCSVRepository : IJobCandidateRepository
    {
        private static string _csvPath;
        private static ConcurrentDictionary<string, JobCandidate> _jobCandidateList 
            = new ConcurrentDictionary<string, JobCandidate>();

        //set timer interval to 10 seconds
        private static System.Timers.Timer _th = new System.Timers.Timer(10000);

        // call once only during start up 
        static JobCandidateCSVRepository()
        {
            _csvPath = Path.Combine(Environment.CurrentDirectory, "JobCandidate.csv");
            //check if file exists and load data from csv
            if (File.Exists(_csvPath))
            {
                var lines = File.ReadAllLines(_csvPath).Skip(1).ToArray();
                foreach (var line in lines)
                {
                    var values = line.Split(',');
                    var jobCandidate = new JobCandidate
                    {
                        FirstName = values[0],
                        LastName = values[1],
                        PhoneNumber = values[2],
                        EmailAddress = values[3],
                        TimeInterval = TimeSpan.Parse(values[4].ToString()),
                        LinkedInUrl = values[5],
                        GitHubUrl = values[6],
                        Comments = values[7],
                    };
                    _jobCandidateList.TryAdd(jobCandidate.EmailAddress, jobCandidate);
                }
            }
            _th.Elapsed += ReplaceCSV;
            _th.Enabled = true;
            _th.AutoReset = true;
            _th.Start();
        }
        public void AddJobCandidate(JobCandidate jobCandidate)
        {
            _jobCandidateList.TryAdd(jobCandidate.EmailAddress, jobCandidate);
        }
        public IEnumerable<JobCandidate> GetAllEmployee()
        {
            return _jobCandidateList.Values.ToList();
        }
        public JobCandidate? GetJobCandidate(string emailAddress)
        {
            JobCandidate? jobCandidate = null;
            _jobCandidateList.TryGetValue(emailAddress, out jobCandidate);
            return jobCandidate;
        }
        public void UpdateJobCandidate(JobCandidate jobCandidateChanges)
        {
            var jobCandidate = GetJobCandidate(jobCandidateChanges.EmailAddress);
            if (jobCandidate == null) return;
            _jobCandidateList.TryUpdate(jobCandidate.EmailAddress, jobCandidateChanges, jobCandidate);  
        }
        private static string CreateCSVHeader()
        {
            PropertyInfo[] propertyinfo;
            propertyinfo = typeof(JobCandidate).GetProperties();
            var header = string.Empty;
            foreach (var prop in propertyinfo)
            {
                if (string.IsNullOrEmpty(header))
                    header += prop.Name;
                else
                    header = string.Format("{0},{1}", header, prop.Name);
            }
            header = string.Format("{0}", header);
            return header;
        }
        private static void ReplaceCSV(Object source, ElapsedEventArgs e)
        {
            _th.Stop();

            var jobCandidates = _jobCandidateList.Values.ToArray();
            var file = new StringBuilder();
            file.AppendLine(CreateCSVHeader());
            var lines = new List<string>();
            for (int i = 0; i < jobCandidates.Length; i++)
            {
                var jobCandidate = jobCandidates[i];
                var split = new List<string>();

                split.Add(jobCandidate.FirstName);
                split.Add(jobCandidate.LastName);
                split.Add(jobCandidate.PhoneNumber);
                split.Add(jobCandidate.EmailAddress);
                split.Add(jobCandidate.TimeInterval.ToString());
                split.Add(jobCandidate.LinkedInUrl);
                split.Add(jobCandidate.GitHubUrl);
                split.Add(jobCandidate.Comments);
                file.AppendLine(string.Join(",", split));                
            }
            File.WriteAllText(_csvPath, file.ToString());

            _th.Start();
        }
    }
}
