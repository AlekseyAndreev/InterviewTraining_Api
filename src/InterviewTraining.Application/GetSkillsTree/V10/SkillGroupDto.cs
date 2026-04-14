using System;
using System.Collections.Generic;

namespace InterviewTraining.Application.GetSkillsTree.V10;

public class SkillGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<SkillGroupDto> ChildGroups { get; set; }
    public List<SkillDto> Skills { get; set; }
}
