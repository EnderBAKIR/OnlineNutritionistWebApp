using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace OnlineNutritionistProject.Models
{
    public class Chat : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IAppuserService _appuserService;

        public Chat(IMessageService messageService, IAppuserService appuserService)
        {
            _messageService = messageService;
            _appuserService = appuserService;
        }

        public async Task SendMessage(string userId, string message, string dietitianId)
        {

            // userId ve dietitianId string, ancak Message'da int olarak bekliyor. Bunları int'e dönüştürüyoruz.
            if (int.TryParse(userId, out int senderId) && int.TryParse(dietitianId, out int receiverId))
            {

                // Gönderen ve alıcı kullanıcı bilgilerini alıyoruz.
                var senderUser = await _appuserService.GetByIdAsync(senderId);
                var receiverUser = await _appuserService.GetByIdAsync(receiverId);

                // Gönderen ve alıcı isimlerini formatlı şekilde alıyoruz.
                string senderName = $"{senderUser.Name} {senderUser.Surname}";
                string receiverName = $"{receiverUser.Name} {receiverUser.Surname}";

                // Yeni mesaj içeriğini "{Gönderen} to {Alıcı}: mesaj" formatında oluşturuyoruz.
                string formattedMessage = $"{senderName} : {message}";

                // Mesajı oluşturuyoruz.
                var existingMessages = await _messageService.GetMessagesByUserIdAsync(senderId);
                var existingConversation = existingMessages
                    .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                               (m.SenderId == receiverId && m.ReceiverId == senderId))
                    .FirstOrDefault();


                if (existingConversation != null)
                {

                    // Mevcut mesajların yanına yeni mesaj ekliyoruz(alt alta).
                    existingConversation.Content += "\n" + formattedMessage;
                    await _messageService.UpdateMessageAsync(existingConversation);
                }


                // Mesajı hem alıcıya hem de göndericiye gönderiyoruz.
                await Clients.User(dietitianId).SendAsync("ReceiveMessage", senderName, message);
                await Clients.User(userId).SendAsync("ReceiveMessage", senderName, message);

            }

        }
    }
}
