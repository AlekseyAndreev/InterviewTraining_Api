using System;
using System.Collections.Generic;

namespace InterviewTraining.Domain;

public class AdditionalUserInfo : BaseDeleteEntity<Guid>
{
    public string IdentityUserId { get; set; }
    public string FullName { get; set; }
    public string PhotoUrl { get; set; }
    public byte[] PhotoLocal { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool IsCandidate { get; set; }
    public bool IsExpert { get; set; }
    /// <summary>
    /// Готов проводить интервью в любое время
    /// </summary>
    public bool InterviewScheduleAtAnyTime { get; set; }

    public Guid? TimeZoneId { get; set; }
    public TimeZone TimeZone { get; set; }

    public List<UserSkill> Skills { get; set; }
    public List<UserRating> MyRatingToUsers { get; set; }
    public List<UserRating> RatingFromUsers { get; set; }
}
