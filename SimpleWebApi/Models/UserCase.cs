using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleWebApi.Model
{
    public class UserCase
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int CaseId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        [ForeignKey(nameof(CaseId))]
        public Case Case { get; set; }
    }
}
