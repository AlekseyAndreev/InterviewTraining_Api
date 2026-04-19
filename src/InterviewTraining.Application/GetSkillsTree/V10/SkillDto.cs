using System;

namespace InterviewTraining.Application.GetSkillsTree.V10;

public class SkillDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
    public bool IsConfirmed { get; set; }
}
