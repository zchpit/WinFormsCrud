using CommonLibrary.Dto;
using System.ComponentModel.DataAnnotations;


namespace SimpleWebApi.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public RoleDto UserRole { get; set; } = RoleDto.User;
        [Required]
        public bool IsActive { get; set; }

        public int? ManagerId { get; set; }

        public ICollection<UserCase> UserCases { get; set; }
    }
}
