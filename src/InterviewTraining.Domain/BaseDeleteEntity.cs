namespace InterviewTraining.Domain;

public class BaseDeleteEntity<T> : BaseEntity<T>
{
    public bool IsDeleted { get; set; }
}
