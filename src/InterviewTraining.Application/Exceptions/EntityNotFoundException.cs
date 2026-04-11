using System;

namespace InterviewTraining.Application.Exceptions;

/// <summary>
/// Exception, when Entity not found
/// </summary>
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName)
        : base($"Entity {entityName} not found")
    {
    }

    public EntityNotFoundException(string entityName, string id)
        : base($"Entity {entityName} not found({id})")
    {
    }
}
