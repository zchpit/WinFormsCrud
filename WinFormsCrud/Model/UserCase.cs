using System.ComponentModel.DataAnnotations;

namespace WinFormsCrud.Model
{
    public class UserCase
    {
        public int UserId { get; set; }
        public int CaseId { get; set; }

        public User User { get; set; }
        public Case Case { get; set; }
    }
}
