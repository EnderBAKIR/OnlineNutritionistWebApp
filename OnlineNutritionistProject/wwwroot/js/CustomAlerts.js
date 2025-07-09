// Ortak SweetAlert Fonksiyonları
const CustomAlerts = {
    // Başarılı işlem alerti
    success: function(title = "Başarılı!", text = "") {
        return Swal.fire({
            title: title,
            text: text,
            icon: "success",
            confirmButtonColor: "#4CAF50",
            confirmButtonText: "Tamam",
            timer: 2000,
            timerProgressBar: true,
            showClass: {
                popup: 'animate__animated animate__fadeInDown'
            },
            hideClass: {
                popup: 'animate__animated animate__fadeOutUp'
            }
        });
    },

    // Hızlı başarı bildirimi (üst köşe)
    quickSuccess: function(title = "Başarılı!") {
        return Swal.fire({
            position: "top-end",
            icon: "success",
            title: title,
            showConfirmButton: false,
            timer: 1500,
            timerProgressBar: true,
            toast: true,
            background: '#4CAF50',
            color: '#fff'
        });
    },

    // Kitap isteği gönderildi
    bookRequestSent: function() {
        return Swal.fire({
            title: "Kitap İsteğiniz Gönderildi!",
            text: "İsteğiniz diyetisyene iletildi. Onaylandığında bilgilendirileceksiniz.",
            icon: "success",
            confirmButtonColor: "#4CAF50",
            confirmButtonText: "Harika!",
            timer: 3000,
            timerProgressBar: true,
            draggable: true
        });
    },

    // PDF indirme başarılı
    pdfDownload: function() {
        return Swal.fire({
            position: "top-end",
            icon: "success",
            title: "Kitabınız indiriliyor...",
            showConfirmButton: false,
            timer: 1500,
            toast: true,
            background: '#2196F3',
            color: '#fff'
        });
    },

    // Hata mesajı
    error: function(title = "Hata!", text = "Bir şeyler ters gitti.") {
        return Swal.fire({
            title: title,
            text: text,
            icon: "error",
            confirmButtonColor: "#f44336",
            confirmButtonText: "Tamam"
        });
    },

    // Onay mesajı
    confirm: function(title = "Emin misiniz?", text = "") {
        return Swal.fire({
            title: title,
            text: text,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#4CAF50",
            cancelButtonColor: "#f44336",
            confirmButtonText: "Evet",
            cancelButtonText: "Hayır"
        });
    }
}; 