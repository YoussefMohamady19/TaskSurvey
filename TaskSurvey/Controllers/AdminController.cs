using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using TaskSurvey.Models;
using TaskSurvey.ViewModel;

namespace TaskSurvey.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly SurvyTaskContext _context=new SurvyTaskContext();

        public async Task<IActionResult> Analytics()
        {
            var questions = await _context.Questions
                .Include(q => q.Options)
                .Where(q => (bool)q.IsActive)
                .ToListAsync();

            var analytics = new List<AnalyticsViewModel>();

            foreach (var question in questions)
            {
                var responses = await _context.UserResponses
                    .Where(r => r.QuestionId == question.QuestionId)
                    .ToListAsync();

                var distribution = new Dictionary<string, int>();

                if (question.QuestionType == "SingleChoice" )
                {
                    foreach (var option in question.Options)
                    {
                        distribution[option.OptionText] = responses
                            .Count(r => r.Answer.Contains(option.OptionText));
                    }
                }

                analytics.Add(new AnalyticsViewModel
                {
                    Question = question,
                    AnswerDistribution = distribution,
                    TotalResponses = responses.Count
                });
            }

            return View(analytics);
        }
        public IActionResult UserInfo()
        {
            var userId_Admin = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = _context.AspNetUsers.ToList();
            List<UserViewModel> viewModes = new List<UserViewModel>();
            foreach (var user in users) 
            {
                if(userId_Admin!=user.Id)
                {
                    UserViewModel viewMode = new UserViewModel()
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        UserName = user.UserName,

                    };
                    viewModes.Add(viewMode);

                }         
            }
            return View(viewModes);
        }
        public async Task<IActionResult> ManageQuestion()
        {
            var questions = await _context.Questions
            .Where(q => (bool)q.IsActive)
            .Include(q => q.Options)
            .ToListAsync();

            var viewModel = new SurveyViewModel
            {
                Questions = questions.Select(q => new QuestionViewModel
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                    QuestionType = q.QuestionType,
                    IsRequired = (bool)q.IsRequired,
                    Options = q.Options.Select(o => new OptionViewModel
                    {
                        OptionId = o.OptionId,
                        OptionText = o.OptionText
                    }).ToList()
                }).ToList()
            };
            return View(viewModel);
        }
        public IActionResult AddQuestion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionViewModel question)
        {
            Question question1= new Question() { 
                QuestionText = question.QuestionText,
                QuestionType = question.QuestionType,
            };
            _context.Questions.Add(question1);
            _context.SaveChanges();
            Question question2 = _context.Questions.OrderByDescending(q => q.QuestionId).FirstOrDefault();
            if (question2.QuestionType !="Text" )
            {
                string[] options = question.Answer.Split(",");
                foreach (string option in options)
                {
                    Option option1 = new Option()
                    {
                        OptionText = option,
                        QuestionId = question2.QuestionId,
                    };
                    _context.Options.Add(option1);
                }
                _context.SaveChanges();
            }
            
            // Redirect to DashBoard or another valid action
            return RedirectToAction("ManageQuestion");  // Or another action like "Questions"
        }
        
        public IActionResult Delete(int id)
        {
            #region Hard Delete from database
            //var questions = await _context.Questions
            //.Where(q => q.QuestionId==id)
            //.Include(q => q.Options)
            //.ToListAsync();
            //if (questions == null)
            //{
            //    return NotFound();

            //}
            //_context.SaveChanges();
            #endregion

            #region Soft Delete rom database
            var questions = _context.Questions.FirstOrDefault(q => q.QuestionId == id);
            if (questions == null)
            {
                return NotFound();

            }
            questions.IsActive = false;
            _context.Update(questions);
            _context.SaveChanges();
            #endregion

            return RedirectToAction("ManageQuestion");  
        }


    }
    
}
