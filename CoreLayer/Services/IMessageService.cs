﻿using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IMessageService
    {
        Task SaveMessageAsync(Message message);
        Task<List<Message>> GetMessagesByUserIdAsync(int userId);
    }
}