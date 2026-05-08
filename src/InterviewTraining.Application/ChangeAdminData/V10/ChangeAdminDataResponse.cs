using System;

namespace InterviewTraining.Application.ChangeAdminData.V10;

public class ChangeAdminDataResponse
{
    /// <summary>
    /// Идентификатор собеседования
    /// </summary>
    public Guid InterviewId { get; set; }

    /// <summary>
    /// Идентификатор новой версии интервью
    /// </summary>
    public Guid NewVersionId { get; set; }

    /// <summary>
    /// Признак успешного выполнения
    /// </summary>
    public bool Success { get; set; }
}
