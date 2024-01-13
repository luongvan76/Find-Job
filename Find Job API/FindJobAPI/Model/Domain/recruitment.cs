using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class recruitment
    {
        //navigation properties: one seeker has many recruitment
        [Key]
        public string? UID { get; set; }
        public seeker? seeker { get; set; }

        //navigation properties: one job has many recruitment
        [Key]
        public int job_id { get; set; }
        public job? job { get; set; }
        public bool? status { get; set; }
        public DateTime registation_date { get; set; }
    }
}
