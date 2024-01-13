using FindJobAPI.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class CreateJob
    {
        [Required(ErrorMessage = "không được bỏ trống Tên công việc")]
        public string? JobTitle { get; set; }
        public string? JobDescription { get; set;}

        [Required(ErrorMessage = "Hãy nhập lương")]
        [DataType(DataType.Currency, ErrorMessage = "Hãy nhập giá trị là tiền tệ")]
        public float Minimum_Salary { get; set; }

        [Required(ErrorMessage = "Hãy nhập lương")]
        [DataType(DataType.Currency, ErrorMessage = "Hãy nhập giá trị là tiền tệ")]
        public float Maximum_Salary { get;set; }
        public string? Requirement { get; set;}

        [Required(ErrorMessage = "không được bỏ trống vị trí")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn lĩnh vực")]
        public int Industry_id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại công việc")]
        public int Type_id { get; set; }

        [Required(ErrorMessage = "Vui lòng thiết lập thời hạn")]
        public string? Deadline { get; set; }

    }

    public class All
    {
        public int id { get; set; }
        public string? employer_name { get; set; }
        public string? title { get; set; }
        public string? posted_date { get; set; }
        public string? status { get; set; }

    }

    public class AllJob
    {
        public int id { get; set; }
        public string? JobTitle { get; set; }
        public string? Location { get; set; }
        public string? Requirement { get; set; }
        public float? Minimum_Salary { get; set; }
        public float? Maximum_Salary { get; set; }
    }

    public class JobDetail
    {
        public string? jobTitle { get; set; }
        public string? jobDescription { get; set; }
        public float minimum_salary { get; set; }
        public float maximum_salary { get; set;}
        public string? requirement { get; set; }
        public string? location { get; set; }
        public string? deadline { get; set; }
        public industry? industry { get; set; }
        public type? type { get; set; }
        public string? posted_date { get; set; }
        public string? employer_name { get; set; }
        public string? email { get; set; }
        public string? website { get; set; }
        public string? contact { get; set; }
        public string? address { get; set; }
        public string? logo { get; set; }
        public string? status { get; set; }
    }

    public class ListJob
    {
        public int id { get; set; }
        public string? jobTitle { get; set; }    
        public float minimum_salary { get; set; }
        public float maximum_salary { get; set; }
        public string? location { get; set; }
        public string? industry { get; set; }
        public string? type { get; set; }
        public string? logo { get; set; }
        public string? deadline { get; set; }
        public string? status { get; set; }
    }

    public class UpdateJob
    {
        public string? JobTitle { get; set; }
        public string? JobDescription { get; set; }
        public float Minimum_Salary { get; set; }
        public float Maximum_Salary { get; set; }
        public string? Requirement { get; set; }
        public string? Location { get; set; }
        public int Industry_id { get; set; }
        public int Type_id { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Thời gian không chính xác")]
        public string? Deadline { get; set; }
    }

    public class ApplyList
    {
        public string? UID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class Search
    {
        public int industry_id { get; set; }
        public int type_id { get; set; }
        public string? location { get; set; }
        public float salary { get; set; }
    }
}
