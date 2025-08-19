namespace Application.Dtos
{
    public class SpeakerDto
    {
        public string? BlogUrl { get; set; }        
        public List<string>? Certifications { get; set; }
        public string? Email { get; set; }
        public string? Employer { get; set; }
        public string? FirstName { get; set; }
        public bool IsBlog { get; set; }
        public string? LastName { get; set; }
        public List<SessionDto>? Sessions { get; set; }
        public WebBrowserDto? WebBrowser { get; set; }
        public int YearsOfExperience {  get; set; }
    }
}
