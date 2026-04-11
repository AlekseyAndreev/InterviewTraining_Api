using System;

namespace InterviewTraining.Domain;

public class UserRating : BaseDeleteEntity<Guid>
{
    public Guid UserFromId { get; set; }
    public AdditionalUserInfo UserFrom { get; set; }
    public Guid UserToId { get; set; }
    public AdditionalUserInfo UserTo { get; set; }
}
