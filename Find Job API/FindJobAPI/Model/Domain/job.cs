using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class job
    {
        [Key]
        public int job_id { get; set; }

        //navigation properties: one job just one employer
        public string? UID { get; set; }
        public employer? employer { get; set; }
        public string? job_title { get; set; }
        public string? job_description { get; set; }
        public float minimum_salary { get; set; }
        public float maximum_salary { get; set; }
        public string? requirement { get; set; }
        public string? location { get; set; }
        public DateTime deadline { get; set; }
        public bool status { get; set; }


        //navigation properties: one job has one industry
        public int industry_id { get; set; }
        public industry? industry { get; set; }

        //navigation properties: one job just one type
        public int type_id { get; set; }
        public type? type { get; set; }
        public DateTime posted_date { get; set; }


        //navigation properties: one job has many recruitment
        public List<recruitment>? recruitment { get; set;}

        //navigation properties: one job has many recruitment_No_Account
        public List<recruitment_no_account>? recruitment_no_account { get; set; }

    }
}
