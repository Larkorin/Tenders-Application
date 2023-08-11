function NewPasswordView() {

    this.ViewName = "NewPasswordView";
    this.ApiService = "Contrasena";

    this.InitView = function () {

        //Asignacion del evento del clic del boton
        $("#btn-NewPassword").click(function () {
            var view = new NewPasswordView();
            view.ChangePassword();
        });
        $("#btnAtras").click(function () {
            localStorage.removeItem("idUsuarioRecover");
            window.location.href = "https://localhost:7071/Login";
        });
    }

    this.ChangePassword = function () {
        if ($('#NewPassword').val() != $('#VerifyNewPassword').val()) {
            Swal.fire({
                title: 'Error con la contraseñas',
                text: 'Las contraseñas no coinciden',
                icon: 'error',
                confirmButtonText: 'Entendido'
            })
        } else {
            if ($('#NewPassword').val().length < 8) {
                Swal.fire({
                    title: 'Error con la contraseña',
                    text: 'Debe de incluir al menos 8 carácteres',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                })

            } else if ($('#NewPassword').val().search(/[a-z]/) < 0) {
                Swal.fire({
                    title: 'Error con la contraseña',
                    text: 'Debe de incluir al menos una letra minúscula.',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                })

            } else if ($('#NewPassword').val().search(/[A-Z]/) < 0) {
                Swal.fire({
                    title: 'Error con la contraseña',
                    text: 'Debe de incluir al menos 1 letra mayúscula.',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                })

            } else if ($('#NewPassword').val().search(/[0-9]/) < 0) {
                Swal.fire({
                    title: 'Error con la contraseña',
                    text: 'Debe de incluir al menos 1 número.',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                })
            } else {
                var data = {};
                var contrasena = $("#NewPassword").val();
                console.log("esta es la contrasena " + contrasena);

                var ctrlActions = new ControlActions();
                console.log("contra: " + $("#NewPassword").val());
                var serviceCreate = this.ApiService + "/CheckLast5Passwords?idUsuario=" + localStorage.getItem("idUsuarioRecover") + "&password=" + contrasena;

                ctrlActions.GetToApi(serviceCreate, function (response) {
                    if (response !== null && response !== undefined) {
                        if (response == true) {
                            console.log("idUsuario: " + localStorage.getItem("idUsuarioRecover"));
                            console.log("contrasena: " + contrasena);

                            serviceCreate = "Contrasena" + "/SetNewPassword?idUsuario=" + localStorage.getItem("idUsuarioRecover") + "&password=" + contrasena;
                            ctrlActions.PostToAPIv1(serviceCreate, data, function () {
                                localStorage.removeItem("idUsuarioRecover");
                                window.location.href = "https://localhost:7071/Login";

                            });
                        } else {
                            console.log("La contraseña no puede ser ninguna de las ultimas 5 contraseñas usadas anteriormente");
                        }
                    }
                    else {
                        console.log("Error buscando contraseñas");
                    }
                });
            }
        }
      
    }
}

//Instanciamiento incial de la clase se ejecuta siempre al finalizar la carga de la vista
$(document).ready(function () {
    var view = new NewPasswordView();
    view.InitView();
})