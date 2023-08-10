using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DAL.Contracts
{
    /// <summary>
    /// This interface is used to perform database operations on Answers Table
    /// </summary>
    public interface IAnswerRepository
    {
        /// <summary>
        /// Creates a new answer for an event question.
        /// </summary>
        /// <param name="answer">Answer Model</param>
        /// <returns>Returns boolean indicating created a response of answer or not</returns>
        public Task<bool> CreateAnswer(Answer answer);
        /// <summary>
        /// Retrieves a list of answers to a specific event question by question ID.
        /// </summary>
        /// <param name="id">The ID of the event question for which the answers are to be retrieved.</param>
        /// <returns>A list of EventQuestionAnswerDTO objects representing the event question answers.</returns>
        public Task<List<EventQuestionAnswerDTO>> GetEventQuestionAnswers(int id);
        /// <summary>
        /// Retrieves a list of all answers to a specific event by event ID and user ID.
        /// </summary>
        /// <param name="eventID">The ID of the event for which the answers are to be retrieved.</param>
        /// <param name="userId">The ID of the user for whom the answers are to be retrieved.</param>
        /// <returns>A list of EventQuestionAnswerDTO objects representing the event answers.</returns>
        public Task<List<EventQuestionAnswerDTO>> GetAllAnswers(int eventID, int userId);

    }
}
