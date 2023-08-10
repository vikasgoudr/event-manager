using EventManager.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.BLL.Contracts
{
    /// <summary>
    /// The IAnswerService interface defines methods for managing answers to event questions.
    /// </summary>
    public interface IAnswerService
    {
        /// <summary>
        /// Creates a new answer for an event question.
        /// </summary>
        /// <param name="answerDTO">The AnswerDTO object containing the answer information.</param>
        /// <returns>True if the answer is created successfully, otherwise false.</returns>
        public Task<bool> CreateAnswer(AnswerDTO answerDTO);

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
