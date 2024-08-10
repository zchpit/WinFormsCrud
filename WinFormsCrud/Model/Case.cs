using System.ComponentModel.DataAnnotations;

namespace WinFormsCrud.Model
{
    public class Case
    {
        [Key]
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }

        [Range(1, 5)]
        public int Priority { get; set; }
        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; } = DateTime.Now;
    }
}
