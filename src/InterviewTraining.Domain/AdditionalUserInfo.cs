using System;
using System.Collections.Generic;

namespace InterviewTraining.Domain;

public class AdditionalUserInfo : BaseDeleteEntity<Guid>
{
    public string IdentityUserId { get; set; }
    public string FullName { get; set; }
    public byte[] PhotoLocal { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public bool IsCandidate { get; set; }
    public bool IsExpert { get; set; }
    public bool IsExpertAvailableInSearch { get; set; }
    public Guid? TimeZoneId { get; set; }
    public TimeZone TimeZone { get; set; }
    public decimal? InterviewPrice { get; set; }
    public Guid? CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public List<UserSkill> Skills { get; set; }
    public List<UserRating> MyRatingToUsers { get; set; }
    public List<UserRating> RatingFromUsers { get; set; }
    public List<UserNotification> Notifications { get; set; }
}
