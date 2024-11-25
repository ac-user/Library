

function ShowToast(message = "") {
    if (message.length > 0) {
        const toastBody = document.querySelector('#errorToast .toast-body');
        toastBody.textContent = message;
    }

    const toastElement = new bootstrap.Toast(document.getElementByClass('toast'));
    toastElement.show();
}