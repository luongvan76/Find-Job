using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class Seeker
    {
        public int id { get; set; }
        public string? job_title { get; set; }
        public float minimum_salary { get; set; }
        public float maximum_salary { get; set; }
        public string? location { get; set; }
        public string? industry { get; set; }
        public string? type { get; set; }
        public string? logo { get; set; }
    } 

}
