using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Dto
{
    public class CaseDeleteDto : IDto
    {
        [Required]
        public required int Id { get; set; }

        [Required]
        public required int DeletedBy { get; set; }

        [Required]
        public required DateTime DeletedDate { get; set; } = DateTime.Now;
    }
}
