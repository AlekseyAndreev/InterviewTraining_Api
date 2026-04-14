namespace InterviewTraining.Application.GetUserInfo.V10;

public class GetUserInfoResponse
{
    public string PhotoUrl { get; set; }
    public byte[] Photo { get; set; }
    public string FullName { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
}
