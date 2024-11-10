var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatNutri")
    .build();

var selectedDietitianId = null;
var selectedUserId = null;

// Diyetisyen seçildiğinde ID'yi al
document.querySelectorAll(".diyetisyen-item").forEach(item => {
    item.addEventListener("click", function () {
        selectedDietitianId = this.getAttribute("data-id");
        selectedUserId = null; // Kullanıcı seçimini sıfırla
        console.log("Seçilen Diyetisyen ID: " + selectedDietitianId);
    });
});

// Kullanıcı seçildiğinde ID'yi al
document.querySelectorAll(".user-item").forEach(item => {
    item.addEventListener("click", function () {
        selectedUserId = this.getAttribute("data-id");
        selectedDietitianId = null; // Diyetisyen seçimini sıfırla
        console.log("Seçilen Kullanıcı ID: " + selectedUserId);
    });
});

// Mesaj gönderme işlevi
document.getElementById("sendButton").addEventListener("click", function (event) {
    var userId = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;

    // Diyetisyen veya kullanıcı ID'sinin seçildiğinden emin ol
    if (!selectedDietitianId && !selectedUserId) {
        alert("Lütfen bir diyetisyen veya kullanıcı seçin.");
        return;
    }

    if (connection.state !== signalR.HubConnectionState.Connected) {
        alert("SignalR bağlantısı kurulamadı. Lütfen tekrar deneyin.");
        return;
    }

    // Hedefe göre mesaj gönder
    connection.invoke("SendMessage", userId, message, selectedDietitianId || selectedUserId)
        .catch(function (err) {
            console.error(err.toString());
        });

    document.getElementById("messageInput").value = ""; // Mesaj alanını temizle
    event.preventDefault();
});

// Bağlantıyı başlat
connection.start().then(function () {
    console.log("SignalR bağlantısı başarıyla kuruldu.");
}).catch(function (err) {
    console.error("Bağlantı hatası: " + err.toString());
});

// Mesaj alma işlemi
connection.on("ReceiveMessage", function (senderName, message) {
    var messageContainer = document.getElementById("messagesList");
    messageContainer.innerHTML += "<li><strong>" + senderName + ":</strong> " + message + "</li>";
});
