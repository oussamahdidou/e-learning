using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Option;
using api.Dtos.Question;
using api.Dtos.Quiz;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Mappers;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{

    public class QuizRepository : IQuizRepository
    {

        private readonly apiDbContext _context;

        public QuizRepository(apiDbContext context)
        {
            _context = context;
        }
        public async Task<Result<QuizDto>> CreateQuiz(CreateQuizDto quizDto)
        {
            try
            {
                Quiz quiz = new Quiz
                {
                    Nom = quizDto.Nom,
                    Statue = quizDto.Statue,
                    Questions = new List<Question>()
                };
                _context.quizzes.Add(quiz);
                foreach (CreateQuestionDto questionDto in quizDto.Questions)
                {
                    Question question = new Question
                    {
                        Nom = questionDto.Nom,
                        Quiz = quiz,
                        Options = new List<Option>()
                    };
                    _context.questions.Add(question);
                    foreach (CreateOptionDto optionDto in questionDto.Options)
                    {
                        Option option = new Option
                        {
                            Nom = optionDto.Nom,
                            Question = question,
                            Truth = optionDto.Truth,
                        };
                        _context.options.Add(option);
                    }
                }

                await _context.SaveChangesAsync();
                return Result<QuizDto>.Success(quiz.ToQuizDto());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return Result<QuizDto>.Failure($"Failed to create quiz: {ex.Message}");
            }

        }
        public async Task<Result<QuizDto>> UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto)
        {
            try
            {
                Quiz? quiz = await _context.quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                    .FirstOrDefaultAsync(q => q.Id == quizId);

                if (quiz == null)
                    return Result<QuizDto>.Failure($"Quiz with id {quizId} not found.");

                quiz.Nom = updateQuizDto.Nom;

                //questions
                foreach (Question question in quiz.Questions.ToList())
                {
                    UpdateQuestionDto? questionDto = updateQuizDto.Questions.FirstOrDefault(q => q.Id == question.Id);

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
                        foreach (Option option in question.Options.ToList())
                        {
                            UpdateOptionDto? optionDto = questionDto.Options.FirstOrDefault(o => o.Id == option.Id);

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
                        foreach (UpdateOptionDto optionDto in questionDto.Options)
                        {
                            if (!question.Options.Any(o => o.Id == optionDto.Id))
                            {
                                // add option exists in DTO but not in the database
                                Option newOption = new Option
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
                foreach (UpdateQuestionDto questionDto in updateQuizDto.Questions)
                {
                    if (!quiz.Questions.Any(q => q.Id == questionDto.Id))
                    {
                        // add question exists in DTO but not in database
                        Question newQuestion = new Question
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

                return Result<QuizDto>.Success(quiz.ToQuizDto());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return Result<QuizDto>.Failure($"Failed to update quiz: {ex.Message}");
            }
        }

        public async Task<Result<QuizDto>> GetQuizById(int id)
        {
            try
            {
                Quiz? quiz = await _context.quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (quiz == null)
                {
                    return Result<QuizDto>.Failure($"Quiz with id {id} not found.");
                }

                var random = new Random();
                var randomQuestions = quiz.Questions.OrderBy(q => random.Next()).Take(10).ToList();

                // Update the quiz object with the selected questions
                quiz.Questions = randomQuestions;
                return Result<QuizDto>.Success(quiz.ToQuizDto());
            }
            catch (Exception ex)
            {
                return Result<QuizDto>.Failure($"Failed to get order: {ex.Message}");
            }
        }

        public async Task<Result<QuizDto>> DeleteQuiz(int id)
        {
            try
            {
                Chapitre? chapitre = await _context.chapitres.FirstOrDefaultAsync(x => x.QuizId == id);
                Quiz? quiz = await _context.quizzes
                .Include(qr => qr.QuizResults)
                .Include(q => q.Questions)
                .ThenInclude(o => o.Options)
                .FirstOrDefaultAsync(x => x.Id == id);
                if (quiz == null || chapitre == null)
                {
                    return Result<QuizDto>.Failure($"quiz with id {id} not found. ");
                }
                chapitre.QuizId = null;
                _context.Remove(quiz);
                await _context.SaveChangesAsync();

                return Result<QuizDto>.Success(quiz.ToQuizDto());
            }
            catch (Exception ex)
            {
                return Result<QuizDto>.Failure($"An error occured while deleting a quiz: {ex.Message}");
            }
        }

        public async Task<Result<Quiz>> Approuver(int id)
        {
            try
            {
                Quiz? quiz = await _context.quizzes.FirstOrDefaultAsync(x => x.Id == id);
                if (quiz == null)
                {
                    return Result<Quiz>.Failure("Quiz not found");
                }
                quiz.Statue = ObjectStatus.Approuver;
                await _context.SaveChangesAsync();
                return Result<Quiz>.Success(quiz);
            }
            catch (System.Exception ex)
            {

                return Result<Quiz>.Failure(ex.Message);

            }
        }

        public async Task<Result<Quiz>> Refuser(int id)
        {
            try
            {
                Quiz? quiz = await _context.quizzes.FirstOrDefaultAsync(x => x.Id == id);
                if (quiz == null)
                {
                    return Result<Quiz>.Failure("Quiz not found");
                }
                quiz.Statue = ObjectStatus.Denied;
                await _context.SaveChangesAsync();
                return Result<Quiz>.Success(quiz);
            }
            catch (System.Exception ex)
            {

                return Result<Quiz>.Failure(ex.Message);

            }
        }
    }


}