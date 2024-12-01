var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatNutri")
    .build();

var selectedDietitianId = null;
var selectedUserId = null;
var selectedMessageId = null; 

// Diyetisyen seçildiğinde ID'yi al ve mesajları filtrele
document.querySelectorAll(".diyetisyen-item").forEach(item => {
    item.addEventListener("click", function () {
        selectedDietitianId = this.getAttribute("data-id");
        selectedUserId = null; 
        selectedMessageId = this.querySelector("input[type='hidden']").value;
        console.log("Seçilen Diyetisyen ID: " + selectedDietitianId);

        showChat();

        filterMessages();
    });
});

// Kullanıcı seçildiğinde ID'yi al ve mesajları filtrele
document.querySelectorAll(".user-item").forEach(item => {
    item.addEventListener("click", function () {
        selectedUserId = this.getAttribute("data-id");
        selectedDietitianId = null;
        selectedMessageId = this.querySelector("input[type='hidden']").value;
        console.log("Seçilen Kullanıcı ID: " + selectedUserId);

        showChat();

        filterMessages();
    });
});

// Chat alanını gösteren fonksiyon
function showChat() {
    const chatContainer = document.querySelector(".chat-container");
    chatContainer.classList.remove("hidden");
}

// Mesajları filtreleyen fonksiyon
function filterMessages() {
    const messagesList = document.getElementById("messagesList");
    const allMessages = messagesList.getElementsByClassName("message-item");

    Array.from(allMessages).forEach(message => {
        const messageSenderId = message.getAttribute("data-person");
        const messageId = message.getAttribute("data-chat");

        // Eğer seçilen bir diyetisyen ya da kullanıcı varsa, ilgili sohbete ait mesajları göster
        if (selectedMessageId && selectedMessageId === messageId) {
            message.classList.remove("hidden"); 
        } else {
            message.classList.add("hidden"); 
        }
    });
}

// Mesaj gönderme işlevi
document.getElementById("sendButton").addEventListener("click", function (event) {
    var userId = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    // Diyetisyen veya kullanıcı ID'sini seçme işlemi.
    if (!selectedDietitianId && !selectedUserId) {
        alert("Lütfen bir diyetisyen veya kullanıcı seçin.");
        return;
    }

    if (connection.state !== signalR.HubConnectionState.Connected) {
        alert("SignalR bağlantısı kurulamadı. Lütfen tekrar deneyin.");
        return;
    }

    connection.invoke("SendMessage", userId, message, selectedDietitianId || selectedUserId, selectedMessageId)
        .catch(function (err) {
            console.error(err.toString());
        });

    document.getElementById("messageInput").value = ""; // Mesaj alanını temizle
    event.preventDefault();
});

connection.start().then(function () {
    console.log("SignalR bağlantısı başarıyla kuruldu.");
}).catch(function (err) {
    console.error("Bağlantı hatası: " + err.toString());
});

// Mesaj alma işlemi
connection.on("ReceiveMessage", function (message, senderId, messageId) {
    var currentUserId = document.getElementById("userInput").value;
    var messageContainer = document.getElementById("messagesList");

    var messageElement = document.createElement("li");
    messageElement.classList.add("message-item");
    messageElement.setAttribute("data-person", senderId);
    messageElement.setAttribute("data-chat", messageId); // Mesaj ID'sini ekle
    if (senderId == currentUserId) {
        messageElement.classList.add("my-message");
    } else {
        messageElement.classList.add("other-message");
    }

    var messageBubble = document.createElement("div");
    messageBubble.classList.add("message-bubble");
    messageBubble.textContent = message;

    messageElement.appendChild(messageBubble);
    messageContainer.appendChild(messageElement);

    messageContainer.scrollTop = messageContainer.scrollHeight;
});