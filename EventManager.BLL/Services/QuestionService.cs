 using AutoMapper;
using EventManager.BLL.Contracts;
using EventManager.DAL.Contracts;
using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.BLL.Services
{

   
    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
         
        public async Task<bool> CreateQuestion(QuestionDTO data)
        {
            Console.WriteLine(data);
            return await _questionRepository.CreateQuestion(_mapper.Map<Question>(data));
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            return await _questionRepository.DeleteQuestion(id);
        }

 
        public async Task<bool> UpdateQuestion(QuestionDTO data)
        {
            return await _questionRepository.UpdateQuestion(_mapper.Map<Question>(data));
        }



      
        async Task<List<QuestionDTO>> IQuestionService.GetQuestions(int id)
        {
            var getQuestions = await _questionRepository.GetQuestions(id);
            return _mapper.Map<List<QuestionDTO>>(getQuestions);
        }
    }
}
