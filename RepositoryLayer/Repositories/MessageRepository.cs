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

        public async Task<List<Message>> GetMessagesByUserIdAsync(int userId)
        {
            var messages = await _DbSet
    .Where(m => m.SenderId == userId || m.ReceiverId == userId)
    .OrderBy(m => m.CreatedDate) // Mesajları gönderilme tarihine göre sıralıyoruz
    .Include(m => m.Sender)   // Gönderen kullanıcının bilgilerini dahil ediyoruz
    .Include(m => m.Receiver) // Alıcı kullanıcının bilgilerini dahil ediyoruz
    .ToListAsync();

            return messages;
        }

        public async Task SaveMessageAsync(Message message)
        {
            await _DbSet.AddAsync(message);
        }
    }
}

