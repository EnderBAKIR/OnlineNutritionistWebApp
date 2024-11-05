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

        public async Task<List<Message>> GetMessagesByUserIdAndNutritionistIdAsync(int userId, int nutriId)
        {
            return await _context.Messages.Where(m => (m.SenderId == userId && m.ReceiverId == nutriId) || (m.SenderId == nutriId && m.ReceiverId == userId)).ToListAsync();
        }

        public async Task SaveMessageAsync(Message message)
        {
            await _DbSet.AddAsync(message);
        }
    }
}

