using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FindJobAPI.Model.DTO
{
    public class CV
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public string? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Experience { get; set; }
        public string? Skills { get; set; }
        public string? Education { get; set; }
        public string? Major { get; set; }
        public string? photo { get; set; }
    }

    public class InforSeeker
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? birthday { get; set; }
        public string? address { get; set; }
        public string? photo { get; set; }
    }
}
