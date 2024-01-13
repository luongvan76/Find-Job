using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class TypeDTO
    {
        public int type_id { get; set; }
        public string? type_name { get; set; }
    }

    public class TypeNoId
    {
        [Required(ErrorMessage = "Please enter type name!")]
        public string? type_name { get; set; }
    }
}
