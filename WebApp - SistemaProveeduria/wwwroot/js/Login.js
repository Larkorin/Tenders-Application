function LoginUser() {

    this.ViewName = "LoginView";
    this.ApiService = "Usuario";
    console.log("Instanciado")

    this.InitView = function () {
        //Asignacion del evento de clic del boton
        $("#btn-Login").click(function () {
            var login = new LoginUser();
            login.Login();
        });

        $("#btn-Recovery").click(function () {
            window.location.href = "https://localhost:7071/passwordrecovery";

        });

        $("#btnAtras").click(function () {
            window.location.href = "https://localhost:7071";
        });
    }

    /*this.Login = function () {

        var user = {};
        user.Email = $("#txtEmail").val();
        user.Password = $("#txtPassword").val();


        console.log("User Login" + JSON.stringify(user));


        var ctrlActions = new ControlActions();

        var serviceRetrieveById = this.ApiService + "/RetrieveByLogin?email=" + user.Email + "&password=" + user.Password;

    

        ctrlActions.GetToApi(serviceRetrieveById, function (response) {

            if (response !== null && response !== undefined) {
                // Accede a la información de la respuesta aquí
                console.log(response);
                console.log(response.id);
                localStorage.setItem("idUsuario", response.id);
                localStorage.setItem("username", response.nombre);
                window.location.href = "https://localhost:7071";
            }
            else {
                console.log("No se pudo obtener el usuario");
                Swal.fire({
                    title: 'Error, el usuario no coincide',
                    text: 'El correo y/o la contraseña no son correctos',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                });
            }
        });
    }*/

    this.Login = function () {
        var user = {};
        user.Email = $("#txtEmail").val();
        user.Password = $("#txtPassword").val();

        console.log("User Login" + JSON.stringify(user));

        var ctrlActions = new ControlActions();

        var serviceRetrieveById = this.ApiService + "/RetrieveByLogin?email=" + user.Email + "&password=" + user.Password;

        ctrlActions.GetToApi(serviceRetrieveById, function (response) {
            if (response !== null && response !== undefined) {
                // Accede a la información de la respuesta aquí
                console.log(response);
                console.log(response.id);
                localStorage.setItem("idUsuario", response.id);
                localStorage.setItem("username", response.nombre);

                const permisosIds = [];
                ctrlActions.GetToApiV2("Usuario/GetUserPermissions?userId=" + response.id, (data) => {    

                    data.forEach(permiso => {
                        if (permiso.estado === "ACTIVO") {
                            if (permiso.id === 1) {
                                permisosIds.push("A");
                            } else {
                                permisosIds.push(permiso.id);
                            }
                            
                        }
                    });
                    
                    localStorage.setItem("permisos", permisosIds);
                    window.location.href = "https://localhost:7071";
                });
            } else {
                console.log("No se pudo obtener el usuario");
                Swal.fire({
                    title: 'Error, el usuario no coincide',
                    text: 'El correo y/o la contraseña no son correctos',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                });
            }
        }, function (error) {
            console.log("Ha ocurrido un error al obtener el usuario: " + error);
            Swal.fire({
                title: 'Error al obtener el usuario',
                text: 'Ha ocurrido un error al obtener el usuario. Asegurese de que los datos ingresados son correctos.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }, function (xhr) {
            if (xhr.status !== 200) {
                console.log("La petición ha fallado con el código de estado " + xhr.status);
                Swal.fire({
                    title: 'Error en la petición',
                    text: 'La petición ha fallado con el código de estado ' + xhr.status,
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                });
            }
        });
    }
}

//Instanciamiento inicial de la clase
//Se ejecuta siempre al finalizar la carga de la vista.
$(document).ready(function () {
    var view = new LoginUser();
    view.InitView();
});