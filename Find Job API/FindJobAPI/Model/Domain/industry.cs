using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class industry
    {
        [Key]
        public int industry_id { get; set; }
        public string? industry_name { get; set; }

        //navigation properties: one industry has many job
        public List<job>? job { get; set; }
    }
}
