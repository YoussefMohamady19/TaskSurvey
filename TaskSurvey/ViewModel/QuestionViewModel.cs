
namespace TaskSurvey.ViewModel
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public List<OptionViewModel> Options { get; set; }
        public string Answer { get; set; }
        public IEnumerable<object> MultiChoiceAnswers { get; internal set; }
    }
}
