using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Quiz;
using api.generique;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{

    public class QuizRepository : IQuizRepository
    {

        private readonly apiDbContext _context;

        public QuizRepository(apiDbContext context)
        {
            _context = context;
        }
        public async Task<Result<Quiz>> CreateQuiz(CreateQuizDto quizDto)
        {
           try{
             var quiz = new Quiz{
                Nom = quizDto.Nom,
                Statue = quizDto.Statue,
                Questions = new List<Question>()
            };
            _context.quizzes.Add(quiz);
            foreach(var questionDto in quizDto.Questions)
            {
                var question = new Question{
                    Nom = questionDto.Nom,
                    Quiz = quiz,
                    Options = new List<Option>()
                };
                _context.questions.Add(question);
                foreach(var optionDto in questionDto.Options)
                {
                    var option = new Option{
                        Nom = optionDto.Nom,
                        Question = question,
                        Truth = optionDto.Truth,
                    };
                    _context.options.Add(option);
                }
            }
            
            await _context.SaveChangesAsync();
            return Result<Quiz>.Success(quiz);
           }catch(Exception ex){
             Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            return Result<Quiz>.Failure($"Failed to create quiz: {ex.Message}");
           }
            
        }
        public async Task<Result<Quiz>> UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto)
        {
            try
            {
                var quiz = await _context.quizzes
                    .Include(q => q.Questions)
                        .ThenInclude(q => q.Options)
                    .FirstOrDefaultAsync(q => q.Id == quizId);

                if (quiz == null)
                    return Result<Quiz>.Failure($"Quiz with id {quizId} not found.");

                quiz.Nom = updateQuizDto.Nom;

                //questions
                foreach (var question in quiz.Questions.ToList())
                {
                    var questionDto = updateQuizDto.Questions.FirstOrDefault(q => q.Id == question.Id);

                    if (questionDto == null)
                    {
                        // delete question that exists in the database but not in DTO
                        _context.questions.Remove(question);
                    }
                    else
                    {
                        // update existing question
                        question.Nom = questionDto.Nom;

                        // handling options for the question
                        foreach (var option in question.Options.ToList())
                        {
                            var optionDto = questionDto.Options.FirstOrDefault(o => o.Id == option.Id);

                            if (optionDto == null)
                            {
                                // delete options that exists in the database but not in DTO
                                _context.options.Remove(option);
                            }
                            else
                            {
                                // update existing option
                                option.Nom = optionDto.Nom;
                                option.Truth = optionDto.Truth;
                            }
                        }

                        // Add new options
                        foreach (var optionDto in questionDto.Options)
                        {
                            if (!question.Options.Any(o => o.Id == optionDto.Id))
                            {
                                // add option exists in DTO but not in the database
                                var newOption = new Option
                                {
                                    Nom = optionDto.Nom,
                                    Truth = optionDto.Truth,
                                    QuestionId = question.Id 
                                };
                                _context.options.Add(newOption);
                            }
                        }
                    }
                }

                // Add new questions
                foreach (var questionDto in updateQuizDto.Questions)
                {
                    if (!quiz.Questions.Any(q => q.Id == questionDto.Id))
                    {
                        // add question exists in DTO but not in database
                        var newQuestion = new Question
                        {
                            Nom = questionDto.Nom,
                            QuizId = quiz.Id,
                            Options = questionDto.Options.Select(o => new Option
                            {
                                Nom = o.Nom,
                                Truth = o.Truth
                            }).ToList()
                        };
                        _context.questions.Add(newQuestion);
                    }
                }

                await _context.SaveChangesAsync();

                return Result<Quiz>.Success(quiz);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return Result<Quiz>.Failure($"Failed to update quiz: {ex.Message}");
            }
        }

        public async Task<Result<Quiz>> GetQuizById(int id)
        {
            try{
                var quiz = await _context.quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if(quiz == null)
                {
                    return Result<Quiz>.Failure($"Quiz with id {id} not found.");
                }

                return Result<Quiz>.Success(quiz);
            }
            catch(Exception ex){
                return Result<Quiz>.Failure($"Failed to get order: {ex.Message}");
            }
        }
    }


}