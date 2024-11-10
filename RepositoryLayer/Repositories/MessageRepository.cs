using CoreLayer.Models;
using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly Context _context;
        private readonly DbSet<Message> _DbSet;

        public MessageRepository(Context context)
        {
            _context = context;
            _DbSet = context.Set<Message>();
        }

        public async Task<List<Message>> GetMessagesByDietitianId(int nutriId)
        {
            // Diyetisyene gelen ve diyetsiyenin gönderdiği tüm mesajları alıyoruz
            var messages = await _context.Messages.Where(m => m.SenderId == nutriId || m.ReceiverId == nutriId).OrderBy(m => m.CreatedDate).Include(m => m.Sender).Include(m => m.Receiver).ToListAsync();

            return messages;
        }

        public async Task<List<Message>> GetMessagesByUserIdAsync(int userId)
        {
            // Üye'ye gelen ve diyetsiyenin gönderdiği tüm mesajları alıyoruz
            var messages = await _DbSet.Where(m => m.SenderId == userId || m.ReceiverId == userId).OrderBy(m => m.CreatedDate).Include(m => m.Sender).Include(m => m.Receiver).ToListAsync();

            return messages;
        }

        public async Task SaveMessageAsync(Message message)
        {
            await _DbSet.AddAsync(message);
        }

        public void UpdateMessageAsync(Message message)
        {
            _DbSet.Update(message);
        }
    }
}

