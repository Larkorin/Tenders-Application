function RegistrationView() {

    this.ViewName = 'RegistrationView';
    this.ApiService = 'Usuario';
    this.OtpRegistration = 'OtpRegistration';
    this.OtpVerification = 'Verify';
    this.cookie = '';
    var self = this;

    this.InitView = () => {
        console.log('Testing the InitView');

        const view = new RegistrationView();

        $('#divVerificarOtp').hide();

        //Asignacion del evento click del boton registrar
        $('#btnRegistrar').click(() => {
            console.log('Testing btnRegistrar');
            view.Register();
        });

        // Agrega un evento para manejar el botón de verificación de OTP
        $('#verify-button').click(() => {
            console.log("Testing verify-button")
            view.VerifyOtp();
        });
    };

    this.Register = () => {

        const formValidation = this.InputsValidation($('#txtCedula').val(), $('#txtNombre').val(), $('#txtPrimerApellido').val(),
            $('#txtSegundoApellido').val(), $('#txtCorreo').val(), $('#txtContrasenna').val(), $('#txtTelefono').val());

        //Validacion del formulario
        if (formValidation != null) {

            // los siguientes iff son para validar los requisitos de la contraseña
            if ($('#txtContrasenna').val().length < 8) {
                Swal.fire({
                    title: 'Error con la contraseña',
                    text: 'Debe de incluir al menos 8 carácteres',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                })

            } else if ($('#txtContrasenna').val().search(/[a-z]/) < 0) {
                Swal.fire({
                    title: 'Error con la contraseña',
                    text: 'Debe de incluir al menos una letra minúscula.',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                })

            } else if ($('#txtContrasenna').val().search(/[A-Z]/) < 0) {
                Swal.fire({
                    title: 'Error con la contraseña',
                    text: 'Debe de incluir al menos 1 letra mayúscula.',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                })

            } else if ($('#txtContrasenna').val().search(/[0-9]/) < 0) {
                Swal.fire({
                    title: 'Error con la contraseña',
                    text: 'Debe de incluir al menos 1 número.',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                })
            }
            //una vez se valida todo esto se procede a constuir el usuario y enviar las solicitudes al API
            const user = {};

            user.id = 0;
            user.cedula = $('#txtCedula').val();
            user.nombre = $('#txtNombre').val();
            user.primerApellido = $('#txtPrimerApellido').val();
            user.segundoApellido = $('#txtSegundoApellido').val();
            user.correo = $('#txtCorreo').val();
            user.telefono = $('#txtTelefono').val();
            user.estado = "string";
            user.fechaRegistro = new Date().toJSON();
            user.permisos = [
                {
                    "id": 0,
                    "nombre": "string",
                    "estado": "string"
                }
            ];
            user.contrasenas = [
                {
                    "id": 0,
                    "valor": $('#txtContrasenna').val(),
                    "estado": "string",
                    "fechaActualizacion": "2023-04-09T02:23:59.509Z",
                    "usuario": "string"
                }
            ];
            

            // var ctrlActions = new ControlActions();

            var data = new FormData();
            data.append('mail', user.Correo);
            data.append('phonenumber', user.Telefono);

            $.ajax({
                type: "POST",
                url: "https://localhost:7123/api/Usuario/OtpRegistration?mail=" + user.correo + "&phonenumber=" + user.telefono,
                xhrFields: {
                    withCredentials: true
                },
                success: function (response) {
                    self.cookie = response;
                    $('#divVerificarOtp').show();
                },
                error: function (xhr, status, error) {
                    console.log("ERROR: " + error);
                }
            });
        }
        else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Por favor complete todos los campos del formulario.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            })
        }
    };

    this.CleanForm = () => {
        $('#txtCedula').val('');
        $('#txtNombre').val('');
        $('#txtPrimerApellido').val('');
        $('#txtSegundoApellido').val('');
        $('#txtCorreo').val('');
        $('#txtContrasenna').val('');
        $('#txtTelefono').val('');
    };

    // Agrega una función para cambiar la vista y mostrar la sección de verificación de OTP
    this.ShowOtpVerification = () => {
        $('#divRegistro').hide();
        $('#divVerificarOtp').show();
    }

    // Agrega una función para verificar el OTP ingresado por el usuario
    this.VerifyOtp = function() {
        const user = {};

        user.Cedula = $('#txtCedula').val();
        user.Nombre = $('#txtNombre').val();
        user.PrimerApellido = $('#txtPrimerApellido').val();
        user.SegundoApellido = $('#txtSegundoApellido').val();
        user.Correo = $('#txtCorreo').val();
        user.Telefono = $('#txtTelefono').val();
        user.Estado = "ACTIVO"; // este valor también puede necesitar ser cambiado
        user.fechaRegistro = new Date().toJSON(); // este campo también necesita ser cambiado
        user.permisos = [];
        user.contrasenas = [];



        var otp = $('#otp').val();
        var password = $('#txtContrasenna').val();

        $.ajax({
            url: "https://localhost:7123/api/Usuario/Verify?otp=" + otp + "&password=" + password + "&cookie=" + self.cookie,
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "*/*"
            },
            data: JSON.stringify({
                "id": 0,
                "cedula": user.Cedula,
                "nombre": user.Nombre,
                "primerApellido": user.PrimerApellido,
                "segundoApellido": user.SegundoApellido,
                "correo": user.Correo,
                "telefono": user.Telefono,
                "estado": user.Estado,
                "fechaRegistro": user.fechaRegistro,
                "permisos": [],
                "contrasenas": []
            }),
            success: function (result) {
                console.log(result);
                window.location.href = "\Login";
            },
            error: function (error) {
                console.error(error);
            }
        });
    }

    this.InputsValidation = (txtCedula, txtNombre, txtPrimerApellido, txtSegundoApellido, txtCorreo, txtContrasenna, txtTelefono) => {
        if (txtCedula === '' || txtNombre === '' || txtPrimerApellido === '', txtSegundoApellido === ''
            || txtCorreo === '' || txtContrasenna === '' || txtTelefono === '') {
            
            return null;
        }
        return 'success';
    }
}

$(document).ready(() => {
    const view = new RegistrationView();
    view.InitView();
})
