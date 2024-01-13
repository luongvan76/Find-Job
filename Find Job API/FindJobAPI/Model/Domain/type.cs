using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class type
    {
        [Key]
        public int type_id { get; set; }
        public string? type_name { get; set; }

        //navigation properties: one type has many job
        public List<job>? jobs { get; set; }
    }
}
