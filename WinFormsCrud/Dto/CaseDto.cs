﻿namespace WinFormsCrud.Dto
{
    public class CaseDto
    {
        public CaseDto() { }

        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
