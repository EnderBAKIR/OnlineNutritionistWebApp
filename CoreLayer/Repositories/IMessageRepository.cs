using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync(Message message);
        Task<List<Message>> GetMessagesByUserIdAsync(int userId);
        Task<List<Message>> GetMessagesByDietitianId(int nutriId);
        void UpdateMessageAsync(Message message);
    }
}
