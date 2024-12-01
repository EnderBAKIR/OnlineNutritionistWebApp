using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IMessageService
    {
        Task<Message> GetMessageByIdAsync(int id);
        Task UpdateMessageAsync(Message message);
        Task SaveMessageAsync(Message message);
        Task<List<Message>> GetMessagesByUserIdAsync(int userId);
        Task<List<Message>> GetMessagesByDietitianId(int nutriId);
        Task<List<Message>> GetMessagesByUserNameAsync(string firstName, string lastName);

    }
}
