function showDiagSuccess(msg) {
    Swal.fire({
        icon: 'success',
        title: 'Éxito',
        text: msg
    })
}

function showDiagError(msg) {
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: msg
    })
}
