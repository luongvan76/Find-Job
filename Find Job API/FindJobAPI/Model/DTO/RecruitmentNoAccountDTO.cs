using FindJobAPI.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
   public class Create
    {
        public int job_id { get; set;}

        [Required(ErrorMessage = "Vui lòng điền tên của bạn")]
        public string? name { get; set;}

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Định dạng email không đúng")]
        public string? email { get; set;}

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Số điện thoại không đúng")]
        public string? phone { get; set;}

        [Required (ErrorMessage = "Vui lòng điền ngày sinh của bạn")]
        [DataType(DataType.Date, ErrorMessage = "Định dạng không đúng ngày tháng năm")]
        public string? birthday { get; set;} 
        public string? address { get; set;}
        public string? major { get; set;}
        public string? experience { get; set;}
        public string? education { get; set;}
        public string? skills { get; set;}
    }

    public class Get
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? birthday { get; set; }
        public string? address { get; set; }
        public string? major { get; set; }
        public string? experience { get; set; }
        public string? education { get; set; }
        public string? skills { get; set; }

    }
}
