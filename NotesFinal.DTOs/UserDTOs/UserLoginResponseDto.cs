namespace NotesFinal.DTOs.UserDTOs
{
    public class UserLoginResponseDto
    {
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
