function PasswordRecoveryView() {
    this.ViewName = 'PasswordRecoveryView';
    this.ApiService = 'Usuario';
    this.cookie;
    var self = this;

    this.InitView = () => {
        localStorage.removeItem("idUsuario");
        localStorage.removeItem("idUsuarioRecover");
        $('#divVerificarOtp').hide();

        $("#btnRecovery").click(function () {
            var view = new PasswordRecoveryView();
            view.BuscarUsuario();
        });

        //Asignacion del evento click del boton registrar
        $('#btn-Verify').click(() => {
            var view = new PasswordRecoveryView();
            view.VerificarOtp(self.cookie); // Pasar self.cookie como parámetro
        });

        // Agrega un evento para manejar el botón de verificación de OTP
        $('#btnAtras').click(() => {
            localStorage.removeItem("idUsuarioRecover");
            window.location.href = "https://localhost:7071/Tenders";
        });
    };

    this.BuscarUsuario = function () {
        console.log("Test buscarUsuario");

        var ctrlActions = new ControlActions();
        var notiType = $("#notification").val();
        var notiContact = $("#notificationContact").val();

        if (notiType == "Email" && notiContact != null) {
            var serviceRetrieveByEmail = this.ApiService + "/RetrieveByEmail?email=" + notiContact;
            ctrlActions.GetToApi(serviceRetrieveByEmail, function (response) {
                if (response !== null && response !== undefined) {
                    // Accede a la información de la respuesta aquí
                    console.log(response);
                    console.log(response.id);
                    localStorage.setItem("idUsuarioRecover", response.id);

                    //Codigo para enviar otp
                    $.ajax({
                        type: "POST",
                        url: "https://localhost:7123/api/Usuario/OtpRegistrationMail?mail=" + notiContact,
                        xhrFields: {
                            withCredentials: true
                        },
                        success: function (response) {
                            //self.cookie = response;
                            $("#cookie").val(response)
                            $('#divVerificarOtp').show();
                        },
                        error: function (xhr, status, error) {
                            console.log("ERROR: " + error);
                        }
                    });
                }
                else {
                    console.log("No se pudo obtener el usuario por email");
                }
            })
        } else if (notiType == "SMS" && notiContact != null) {
            var serviceRetrieveByPhone = this.ApiService + "/RetrieveByPhone?phone=" + notiContact;
            ctrlActions.GetToApi(serviceRetrieveByPhone, function (response) {
                if (response !== null && response !== undefined) {
                    // Accede a la información de la respuesta aquí
                    console.log(response);
                    console.log(response.id);
                    localStorage.setItem("idUsuarioRecover", response.id);

                    //Codigo para enviar otp
                    $.ajax({
                        type: "POST",
                        url: "https://localhost:7123/api/Usuario/OtpRegistrationPhone?phonenumber=" + notiContact,
                        xhrFields: {
                            withCredentials: true
                        },
                        success: function (response) {
                            //self.cookie = response;
                            $("#cookie").val(response);
                            $('#divVerificarOtp').show();

                        },
                        error: function (xhr, status, error) {
                            console.log("ERROR: " + error);
                        }
                    });
                }
                else {
                    console.log("No se pudo obtener el usuario por telefono");
                }
            })
        } else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Debe de poner un metodo de contacto',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }
    }


    this.VerificarOtp = function (cookie) { // Añadir cookie como parámetro
        var otp = $('#RecoveryCode').val();
        var cookie = $("#cookie").val();

        $.ajax({
            url: "https://localhost:7123/api/Usuario/VerifyRecover?otp=" + otp + "&cookie=" + cookie,
            method: "POST",
            data: {
            },
            success: function (result) {
                console.log(result);
                window.location.href = "https://localhost:7071/newpassword";
            },
            error: function (error) {
                console.error(error);
            }
        });
    };
}

$(document).ready(function () {
    var view = new PasswordRecoveryView();
    view.InitView();
});