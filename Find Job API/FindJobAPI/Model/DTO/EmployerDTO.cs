using FindJobAPI.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class EmployerDTO
    {
        public string? employer_name { get; set; }
        public string? email { get; set; }
        public string? contact_phone { get; set; }
        public string? employer_website { get; set; }
        public string? employer_address { get; set; }
        public string? employer_about { get; set; }
        public string? employer_image { get; set; }
        public string? image_cover { get; set; }
    }
    public class Image
    {
        public string? image { get; set; }
    }

    public class Cover
    {
        public string? imageCover { get; set; }
    }

    public class InforEmployer
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }   
        public string? About { get; set; }
    }
}
