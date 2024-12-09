function deleteMessage(messageId, isForUser) {
    // Üye ise,
    if (isForUser) {
        if (confirm('Bu mesajı silmek istediğinizden emin misiniz?')) {
            $.ajax({
                url: '/Chat/RemoveMessageForUser/' + messageId, 
                type: 'POST',
                success: function (response) {
                    if (response.success) {

                        Swal.fire({
                            title: 'Silindi!',
                            text: 'Mesaj başarıyla silindi.',
                            icon: 'success',
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            
                            location.reload();
                        });
                    } else {
                        alert('Mesaj silinemedi. Lütfen tekrar deneyin.');
                    }
                },
                error: function (xhr, status, error) {
                    alert("Bir hata oluştu. Lütfen tekrar deneyin.");
                }
            });
        }
    } else {
        // Eğer Nutri ise,
        Swal.fire({
            title: 'Mesajı Silmek İstediğinizden Emin Misiniz?',
            text: "Bu işlemi geri alamazsınız!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Evet, Sil!',
            cancelButtonText: 'Hayır, İptal',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Chat/RemoveMessageForNutri/' + messageId,
                    type: 'POST',
                    success: function (response) {
                        if (response.success) {
                            Swal.fire({
                                title: 'Silindi!',
                                text: 'Mesaj başarıyla silindi.',
                                icon: 'success',
                                timer: 2000,
                                showConfirmButton: false
                            }).then(() => {
                                location.reload();
                            });
                        } else {
                            Swal.fire(
                                'Hata!',
                                'Mesaj silinemedi. Lütfen tekrar deneyin.',
                                'error'
                            );
                        }
                    },
                    error: function (xhr, status, error) {
                        Swal.fire(
                            'Hata!',
                            'Bir hata oluştu. Lütfen tekrar deneyin.',
                            'error'
                        );
                    }
                });
            }
        });
    }
}