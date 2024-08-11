namespace CommonLibrary.Dto
{
    public class SimpleUserDto
    {
        public int Id { get; set; }
        public RoleDto UserRole { get; set; } = RoleDto.User;
    }
}
