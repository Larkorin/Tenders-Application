function PermissionsView() {

    this.ViewName = "PermissionsView";
    this.ApiService = "Permiso";

    var self = this;

    this.InitView = function () {

        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new PermissionsView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new PermissionsView();
            view.Update();
        });

        $("#btnCancel").click(function () {
            var view = new PermissionsView();
            view.CleanForm();
        });

        this.LoadPermissionsTable();
    }

    this.Create = function () {

        const formValidation = this.InputsValidation($("#txtPermissionName").val());

        if (formValidation) {
            var permiso = {};
            permiso.Nombre = $("#txtPermissionName").val();
            permiso.Estado = "";

            var bitacora = {};
            bitacora.Descripcion = "Se creo un nuevo permiso.";
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPIv1(serviceCreate, permiso, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Permiso creado',
                        text: '¡El permiso se ha creado exitosamente!',
                        icon: 'success',
                        confirmButtonText: 'Entendido'
                    });
                });
            });
        } else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Por favor complete todos los campos del formulario.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }
    }

    this.Update = function () {

        const formValidation = this.InputsValidation($("#txtPermissionName").val());

        if (formValidation) {
            var permiso = {};
            permiso.Id = $("#txtId").val();
            permiso.Nombre = $("#txtPermissionName").val();
            permiso.Estado = "";

            var bitacora = {};
            bitacora.Descripcion = "Se modifico el permiso de ID: " + permiso.Id;
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, permiso, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Permiso actualizado',
                        text: '¡El permiso se ha actualizado exitosamente!',
                        icon: 'success',
                        confirmButtonText: 'Entendido'
                    });
                }); 
            });
        } else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Por favor complete todos los campos del formulario.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }
    }

    this.InputsValidation = (txtNombrePermiso) => {
        if (txtNombrePermiso === '') {

            return false;
        }
        return true;
    }

    this.LoadPermissionsTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblPermissions')) {
            // Destruir la instancia existente
            $('#tblPermissions').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }


        // Crear la instancia de la tabla tblPermissions con los datos vacíos
        var table = $('#tblPermissions').DataTable({
            "data": [],
            "columns": arrayColumnsData
        });

        // Actualizar la tabla con los nuevos datos
        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                table.rows.add(data).draw();
            },
            error: function (error) {
                console.log(error);
            }
        });

        $('#tblPermissions tbody').on('click', 'tr', function () {

            var tr = $(this).closest('tr');
            var data = $('#tblPermissions').DataTable().row(tr).data();

            $("#txtId").val(data.id);
            $("#txtPermissionName").val(data.nombre);

            $("#btnCreate").prop('disabled', true);
            $("#btnUpdate").prop('disabled', false);
        });
    }

    this.CleanForm = function () {
        $("#txtPermissionName").val("");
        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);
        
        self.LoadPermissionsTable();
    }
}

$(document).ready(function () {

    var view = new PermissionsView();
    view.InitView();

})