using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class IndustryDTO
    {
        public int industry_id { get; set; }
        public string? industry { get; set; }
    }

    public class IndustryNoId
    {
        [Required(ErrorMessage = "Please enter industry!")]
        public string? industry { get; set; }
    }
}
