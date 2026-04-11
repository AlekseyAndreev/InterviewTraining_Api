using System.Collections.Generic;

namespace InterviewTraining.Application.Common;

public class PagedResponse<T>
{
    public IReadOnlyCollection<T> Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
}
