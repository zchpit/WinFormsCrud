using System.ComponentModel.DataAnnotations;

namespace SimpleWebApi.Model
{
    public class Case
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Header { get; set; }
        [Required]
        public string Description { get; set; }

        [Range(1, 5)]
        [Required]
        public int Priority { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        public ICollection<UserCase> UserCases { get; set; }
    }
}
