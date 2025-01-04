using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskSurvey.Models;
using TaskSurvey.ViewModel;

namespace TaskSurvey.Controllers
{
    public class SurveyController : Controller
    {
        private readonly SurvyTaskContext _context=new SurvyTaskContext();


        // Add this attribute to protect controllers/actions
        [Authorize]  
        public async Task<IActionResult> SurveyQuestions()

        {
            //Get List From Questions and Answers Options
            var questions = await _context.Questions
            .Where(q => (bool)q.IsActive)
            .Include(q => q.Options)
            .ToListAsync();
            //Create List to Send Data for View
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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Submit(SurveyViewModel model)
        {
            try
            {
                

                // Get all valid question IDs from database
                var validQuestionIds = await _context.Questions
                    .Where(q => (bool)q.IsActive)
                    .Select(q => q.QuestionId)
                    .ToListAsync();

                foreach (var question in model.Questions)
                {
                    // Validate if QuestionId exists
                    if (!validQuestionIds.Contains(question.QuestionId))
                    {
                        ModelState.AddModelError("", $"Invalid Question ID: {question.QuestionId}");
                        return View("SurveyQuestions", model); // If invalid question ID, show the error and return to form.
                    }

                    // Validate required questions
                    if (question.IsRequired && string.IsNullOrWhiteSpace(question.Answer))
                    {
                        ModelState.AddModelError("", "Please answer all required questions");
                        return View("SurveyQuestions", model); // If required questions are not answered, return to form.
                    }

                    if (question.QuestionType == "SingleChoice" || question.QuestionType == "Text")
                    {
                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                        // For SingleChoice or Text-based questions, save the single answer.
                        var response = new UserResponse
                        {
                            UserId = userId,
                            QuestionId = question.QuestionId,
                            Answer = question.Answer ?? "", // Handle null answers
                            SubmittedDate = DateTime.Now
                        };

                        _context.UserResponses.Add(response);
                    }
                    
                }

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect to a thank-you page after successful submission
                return RedirectToAction("ThankYou");
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "An error occurred while saving your responses.");
                return View("SurveyQuestions", model); 
            }
        }
        public IActionResult ThankYou()
        {
            // This will return a Thank You view after form submission.
            return View(); 
        }

    }
}