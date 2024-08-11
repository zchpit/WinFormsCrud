using System.ComponentModel.DataAnnotations;

namespace SimpleWebApi.Model
{
    public class UserCase
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CaseId { get; set; }

        public User User { get; set; }
        public Case Case { get; set; }
    }
}
