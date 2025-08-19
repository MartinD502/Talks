namespace Utilities
{
    public enum Status
    {
        Success = 1,
        Error = 2
    }   

    public enum WebBrowser
    {
        Chrome = 1,
        InternetExplorer = 2,
        Firefox = 3,
        Opera = 4,
        Safari = 5,
        Other = 6
    }

    public enum RegistrationResult
    {
        EmailRequired=1,
        FirstNameRequired=2,
        LastNameRequired =3,
        NoSessionsApproved = 4,
        NoSessionsProvided = 5,
        SpeakerDoesNotMeetStandards = 6,
        Success = 7
    }
}
