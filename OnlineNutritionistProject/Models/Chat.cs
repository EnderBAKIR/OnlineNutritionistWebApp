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

        public async Task SendMessage(string userId, string message, string dietitianId, string messageId)
        {
            // userId, dietitianId string, ancak Message'da int olarak bekliyor. Bunları int'e dönüştürüyoruz.
            if (int.TryParse(userId, out int senderId) && int.TryParse(dietitianId, out int receiverId) && int.TryParse(messageId, out int selectedMessageId))
            {
                // Gönderen ve alıcı kullanıcı bilgilerini alıyoruz.
                var senderUser = await _appuserService.GetByIdAsync(senderId);
                var receiverUser = await _appuserService.GetByIdAsync(receiverId);

                // Gönderen ve alıcı isimlerini formatlı şekilde alıyoruz.
                string senderName = $"{senderUser.Name} {senderUser.Surname}";
                string receiverName = $"{receiverUser.Name} {receiverUser.Surname}";

                // Yeni mesaj içeriğini "{Gönderen} to {Alıcı}: mesaj" formatında oluşturuyoruz.
                string formattedMessage = $"{senderName} : {message}";

                // Burada sadece seçilen messageId ile eşleşen konuşmayı buluyoruz
                var existingConversation = await _messageService.GetMessageByIdAsync(selectedMessageId);

                if (existingConversation != null)
                {
                    // Mevcut mesajların yanına yeni mesaj ekliyoruz(alt alta).
                    existingConversation.Content += "\n" + formattedMessage;
                    await _messageService.UpdateMessageAsync(existingConversation);
                }

                // Mesajı hem alıcıya hem de göndericiye gönderiyoruz.
                await Clients.User(dietitianId).SendAsync("ReceiveMessage", message, senderId, selectedMessageId);
                await Clients.User(userId).SendAsync("ReceiveMessage", message, senderId, selectedMessageId);
            }
        }

    }
}