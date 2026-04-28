using InterviewTraining.Application.CustomMediatorLogic;

namespace InterviewTraining.Application.GetAllUsersForAdmin.V10;

///<summary>
/// Request to get all users (admin only)
///</summary>
public class GetAllUsersForAdminRequest : IMediatorRequest<GetAllUsersForAdminResponse>
{
    ///<summary>
    /// Page number (1-based)
    ///</summary>
    public int PageNumber { get; set; } = 1;

    ///<summary>
    /// Page size
    ///</summary>
    public int PageSize { get; set; } = 20;

    ///<summary>
    /// Optional search filter
    ///</summary>
    public string SearchFilter { get; set; }

    public bool IsAdmin { get; set; }
}
