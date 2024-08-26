using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Dto
{
    public class CaseUpdateDto
    {

        [Required]
        public required int Id { get; set; }

        public string? Header { get; set; }

        public string? Description { get; set; }

        [Range(1, 5)]
        public int? Priority { get; set; }

        [Required]
        public required int LastModifiedBy { get; set; }

        public required DateTime LastModifiedDate { get; set; }
    }
}
