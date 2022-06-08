namespace CleanArchitecture.WebApi.Model
{
    public class UserLoginResponseDto
    {
        public string Token { get; set; }
        public bool Login { get; set; }
        public List<string> Errors { get; set; }
    }
}
