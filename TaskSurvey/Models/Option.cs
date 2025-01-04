using System;
using System.Collections.Generic;

namespace TaskSurvey.Models;

public partial class Option
{
    public int OptionId { get; set; }

    public int? QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public virtual Question? Question { get; set; }
}
