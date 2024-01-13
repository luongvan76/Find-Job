using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class seeker
    {
        [Key]
        public string? UID { get; set; }

        //navigation properties: one seeker has one account
        public account? account { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? birthday { get; set; }
        public string? address { get; set; }
        public string? experience { get; set; }
        public string? skills { get; set; }
        public string? education { get; set; }
        public string? major { get; set; }

        //navigation properties: one seeker has many recruitment
        public List<recruitment>? recruitments { get; set; }

    }
}
