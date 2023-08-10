using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.BLL.Contracts
{
    /// <summary>
    /// The IQuestionService interface defines methods for managing questions.
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// Creates a new question.
        /// </summary>
        /// <param name="data">The QuestionDTO object containing the question information.</param>
        /// <returns>True if the question is created successfully, otherwise false.</returns>
        public Task<bool> CreateQuestion(QuestionDTO data);

        /// <summary>
        /// Updates an existing question.
        /// </summary>
        /// <param name="data">The QuestionDTO object containing the updated question information.</param>
        /// <returns>True if the question is updated successfully, otherwise false.</returns>
        public Task<bool> UpdateQuestion(QuestionDTO data);

        /// <summary>
        /// Retrieves a list of questions for a specific event by event ID.
        /// </summary>
        /// <param name="id">The ID of the event for which the questions are to be retrieved.</param>
        /// <returns>A list of QuestionDTO objects representing the event questions.</returns>
        public Task<List<QuestionDTO>> GetQuestions(int id);

        /// <summary>
        /// Deletes a question by question ID.
        /// </summary>
        /// <param name="id">The ID of the question to be deleted.</param>
        /// <returns>True if the question is deleted successfully, otherwise false.</returns>
        public Task<bool> DeleteQuestion(int id);
    }
}
