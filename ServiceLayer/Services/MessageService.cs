using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using CoreLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Message>> GetMessagesByUserIdAsync(int userId)
        {
           return await _messageRepository.GetMessagesByUserIdAsync(userId);
        }

        public async Task SaveMessageAsync(Message message)
        {
            message.CreatedDate = DateTime.Now;
            await _messageRepository.SaveMessageAsync(message);
            await _unitOfWork.CommitAsync();
        }

    }
}
