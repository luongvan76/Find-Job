using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class recruitment_no_account
    {
        [Key]
        public int recruitment_ID { get; set; }

        //navigation properties: one job has one recruitment_no_account
        public int job_id { get; set; }
        public job? job {  get; set; }

        public string? fullname { get; set; }
        public string? email { get; set; }
        public string? phone_number { get; set; }
        public DateTime? birthday { get; set; } 
        public string? address { get; set; }
        public string? experience { get; set; }
        public string? skills { get; set; }
        public string? education { get; set; }
        public string? major { get; set; }
        public bool? status { get; set; }
        public DateTime registration_date { get; set; }


    }
}
