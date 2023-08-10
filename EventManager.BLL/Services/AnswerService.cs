using AutoMapper;
using EventManager.BLL.Contracts;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.DAL.Repositories;
using EventManager.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.BLL.Services
{
 
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        
        public Task<bool> CreateAnswer(AnswerDTO answerDTO)
        {
            answerDTO.IsDeleted = false;
            return _answerRepository.CreateAnswer(_mapper.Map<Answer>(answerDTO));
        }
 
        public async Task<List<EventQuestionAnswerDTO>> GetEventQuestionAnswers(int userId)
        {
            return await _answerRepository.GetEventQuestionAnswers(userId);
        }

        
        public async Task<List<EventQuestionAnswerDTO>> GetAllAnswers(int eventID, int userId)
        {
            return await _answerRepository.GetAllAnswers(eventID, userId);
        }
    }
}
