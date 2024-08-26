using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Dto
{
    public class CaseCreateDto : IDto
    {
        public CaseCreateDto() { }

        [Required]
        public required string Header { get; set; }

        [Required]
        public required string Description { get; set; }

        [Range(1, 5)]
        [Required]
        public required int Priority { get; set; }

        [Required]
        public required int CreatedBy { get; set; }

        public required DateTime CreateDate { get; set; }

        [Required]
        public required int LastModifiedBy { get; set; }

        public required DateTime LastModifiedDate { get; set; }
    }
}
