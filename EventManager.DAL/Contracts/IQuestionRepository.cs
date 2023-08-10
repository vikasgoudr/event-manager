using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Contracts
{
    /// <summary>
    /// This interface is used to perform database operations on Question Table
    /// </summary>
    public interface IQuestionRepository
    {
        /// <summary>
        /// Creates a custom form question based on given information
        /// </summary>
        /// <param name="question">Question Model</param>
        /// <returns>Returns boolean indicating created a question for custom form or not</returns>
        public Task<bool> CreateQuestion(Question question);
        /// <summary>
        /// Updates a custom form question based on given question model
        /// </summary>
        /// <param name="question">Question Model</param>
        /// <returns>Returns boolean indicating updated a question for custom form or not</returns>
        public Task<bool> UpdateQuestion(Question question);
        /// <summary>
        /// Method used to get all questions for custom form
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>Returns list of questions for custom form creation</returns>
        public Task<List<Question>> GetQuestions(int id);
        /// <summary>
        /// Deletes a question in the question table
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>Returns boolean indicating deleted a question or not</returns>
        public Task<bool> DeleteQuestion(int id);
    }
}
