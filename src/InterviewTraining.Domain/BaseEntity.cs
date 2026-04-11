using System;

namespace InterviewTraining.Domain;

public abstract class BaseEntity<T>
{
    public T Id { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime? ModifiedUtc { get; set; }
}