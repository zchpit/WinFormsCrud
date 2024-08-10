namespace WinFormsCrud.Dto
{
    public class UserDto
    {
        public UserDto() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleDto UserRole { get; set; } = RoleDto.User;
        public bool IsActive { get; set; }
        public List<CaseDto> UserCases { get; set; } = new List<CaseDto>();
    }
}
