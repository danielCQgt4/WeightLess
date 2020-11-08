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

//function showDiagConfirm(msg) {
//    Swal.fire({
//        title: 'Estas seguro?',
//        text: msg,
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Continuar',
//        cancelButtonText: 'Cancelar'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            //Swal.fire(
//            //    'Deleted!',
//            //    'Your file has been deleted.',
//            //    'success'
//            //)
//        }
//    })
//}