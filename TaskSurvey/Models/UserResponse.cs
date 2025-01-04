using System;
using System.Collections.Generic;

namespace TaskSurvey.Models;

public partial class UserResponse
{
    public int ResponseId { get; set; }

    public string? UserId { get; set; }

    public int? QuestionId { get; set; }

    public string? Answer { get; set; }

    public DateTime? SubmittedDate { get; set; }

    public virtual Question? Question { get; set; }

    public virtual AspNetUser? User { get; set; }
}
