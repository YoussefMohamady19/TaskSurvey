using TaskSurvey.Models;

namespace TaskSurvey.ViewModel
{
    public class AnalyticsViewModel
    {
        public Question Question { get; set; }
        public Dictionary<string, int> AnswerDistribution { get; set; }
        public int TotalResponses { get; set; }
    }
}
