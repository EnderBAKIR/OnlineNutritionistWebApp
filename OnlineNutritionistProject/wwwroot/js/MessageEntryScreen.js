document.addEventListener('DOMContentLoaded', function () {
    const noChatSelected = document.querySelector('.no-chat-selected');
    const diyetisyenItems = document.querySelectorAll('.diyetisyen-item, .user-item');
    const messagesList = document.getElementById('messagesList');

    noChatSelected.style.display = 'flex';

    diyetisyenItems.forEach(function (item) {
        item.addEventListener('click', function () {
            if (noChatSelected) {
                noChatSelected.style.display = 'none';
            }

            messagesList.style.display = 'block'; 
        });
    });
});