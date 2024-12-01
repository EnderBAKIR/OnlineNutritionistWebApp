document.addEventListener("DOMContentLoaded", function () {
    const messagesList = document.getElementById("messagesList");

    const infoText = document.createElement("li");
    infoText.style.textAlign = "center";
    infoText.style.color = "#666";
    infoText.style.marginTop = "10px";
    infoText.innerHTML = "<em>Önceki mesajlarınız...</em>";

    if (messagesList.children.length > 0) {
        messagesList.appendChild(infoText);


        setTimeout(() => {
            infoText.remove();
        }, 3000);
    }
});

document.querySelectorAll(".diyetisyen-item").forEach(item => {
    item.addEventListener("click", function () {
        for (i = 0; i < document.querySelectorAll(".diyetisyen-item").length; i++) {
            document.querySelectorAll(".diyetisyen-item")[i].classList.remove("active");
        }
        item.classList.add("active");
    });
});

document.querySelectorAll(".user-item").forEach(item => {
    item.addEventListener("click", function () {
        for (i = 0; i < document.querySelectorAll(".user-item").length; i++) {
            document.querySelectorAll(".user-item")[i].classList.remove("active");
        }
        item.classList.add("active");
    });
});
