using InterviewTraining.Application.GetAllExperts.V10;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Mappers;

public static class AdditionalUserInfoMapper
{
    public static GetExpertResponse ToGetExpertResponse(this AdditionalUserInfo additionalUserInfo)
    {
        if (additionalUserInfo == null)
        {
            return null;
        }

        return new GetExpertResponse
        {
            Id = additionalUserInfo.Id,
            IdentityServerId = additionalUserInfo.IdentityUserId,
            Name = additionalUserInfo.FullName,
        };
    }
}
