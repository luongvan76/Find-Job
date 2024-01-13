using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class account
    {
        [Key]
        public string? UID { get; set; }

        //navigation properties: one account just has seeker or employer
        public List<seeker>? seekers { get; set; }
        public List<employer>? employers { get; set; }
    }
}
