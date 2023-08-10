using EventManager.DAL.Context;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly EventManagerDbContext _context;

        public QuestionRepository(EventManagerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateQuestion(Question question)
        {
            await _context.Questions.AddAsync(question);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            var question = await _context.Questions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (question == null)
            {
                return false;
            }
            question.IsDeleted = true;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Question>> GetQuestions(int id)
        {
            var eventForm = await _context.Questions.Where(x => x.EventId == id && !x.IsDeleted).OrderBy(x => x.Id).ToListAsync(); 
            if(eventForm!=null)
            {
                return eventForm;
            }
            return new List<Question>();
        }

        public async Task<bool> UpdateQuestion(Question question)
        {
            var updateForm = await _context.Questions.FirstOrDefaultAsync(x => x.Id == question.Id && !x.IsDeleted);
            if(updateForm!=null)
            {
                updateForm.Type = question.Type;
                updateForm.Name = question.Name;
                updateForm.Options = question.Options;
                updateForm.defaultOption = question.defaultOption;
                updateForm.defaultValue = question.defaultValue;
                updateForm.IsRequired = question.IsRequired;
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
