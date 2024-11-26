

function showToast(message = "") {
    if (message.length > 0) {
        const toastBody = document.querySelector('#errorToast .toast-body');
        toastBody.textContent = message;
    }

    const element = document.getElementsByClassName('toast')[0];
    //element.className += 'show';
    const toast = new bootstrap.Toast(element);
    toast.show();
}