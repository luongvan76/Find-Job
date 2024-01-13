using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class employer
    {
        [Key]
        public string? UID { get; set; }

        //navigation properties: one employer has one account
        public account? account { get; set; }
        public string? employer_name { get; set; }
        public string? email { get; set; }
        public string? contact_phone { get; set; }
        public string? employer_website { get; set; }
        public string? employer_address { get; set; }
        public string? employer_about { get; set;}
        public string? employer_image { get; set; }
        public string? employer_image_cover { get; set; }


        //navigation properties: one employer has many job
        public List<job>? jobs { get; set; }
    }
}
