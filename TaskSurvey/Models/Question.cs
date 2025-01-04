using System;
using System.Collections.Generic;

namespace TaskSurvey.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string QuestionType { get; set; } = null!;

    public bool? IsRequired { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual ICollection<UserResponse> UserResponses { get; set; } = new List<UserResponse>();
}
