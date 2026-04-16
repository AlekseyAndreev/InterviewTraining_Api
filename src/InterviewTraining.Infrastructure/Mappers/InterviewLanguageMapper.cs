using InterviewTraining.Application.GetAllInterviewLanguages.V10;
using InterviewTraining.Domain;

namespace InterviewTraining.Infrastructure.Mappers;

public static class InterviewLanguageMapper
{
    public static InterviewLanguageResponse ToInterviewLanguageResponse(this InterviewLanguage entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new InterviewLanguageResponse
        {
            Id = entity.Id,
            Code = entity.Code,
            NameEn = entity.NameEn,
            NameRu = entity.NameRu,
        };
    }
}