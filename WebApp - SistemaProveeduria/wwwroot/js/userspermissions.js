function UsersPermissionsView() {

    this.ViewName = "UsersPermissionsView";
    this.UserApiService = "Usuario";
    
    var self = this;

    this.InitView = function () {

        $("#btnAssignPermissions").prop('disabled', true);
        $("#btnRemovePermissions").prop('disabled', true);
        $("#btnCancel").prop('disabled', true);

        $("#userId").prop('disabled', true);
        $("#userName").prop('disabled', true);

        $("#btnAssignPermissions").click(function () {
            var view = new UsersPermissionsView();
            view.AssignPermissions();
        });

        $("#btnGoToPermissions").click(function () {
            var view = new UsersPermissionsView();
            view.GoToPermissions();
        });

        $("#btnRemovePermissions").click(function () {
            var view = new UsersPermissionsView();
            view.RemovePermissions();
        });

        $("#btnCancel").click(function () {
            var view = new UsersPermissionsView();
            view.CleanForm();
        });

        this.LoadUsersTable();
        this.LoadPermissionsTable();    
    }

    this.AssignPermissions = function () {

        var checkedPermissions = [];

        // Obtener los valores de id de las filas seleccionadas
        $('#tblPermissions tbody input[type="checkbox"]:checked').each(function () {
            var row = $(this).closest('tr');
            var id = row.find('td:eq(0)').text();
            checkedPermissions.push(id);
        });

        // Enviar los datos al servicio web
        var ctrlActions = new ControlActions();
        var serviceCreate = this.UserApiService + "/AssignPermissions?userId=" + $("#userId").val();

        var bitacora = {};
        bitacora.Descripcion = "Se asignaron permisos al usuario de ID: " + $("#userId").val();
        bitacora.UsuarioId = localStorage.getItem("idUsuario");
        bitacora.FechaRegistro = new Date();

        ctrlActions.PostToAPIv1(serviceCreate, checkedPermissions, function () {
            ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                self.CleanForm();
                Swal.fire({
                    title: 'Permisos asignados',
                    text: '¡Los permisos se han asignado exitosamente!',
                    icon: 'success',
                    confirmButtonText: 'Entendido'
                });
            });
        });
    }

    this.RemovePermissions = function () {

        var checkedPermissions = [];

        // Obtener los valores de id de las filas seleccionadas
        $('#tblPermissions tbody input[type="checkbox"]:not(:checked)').each(function () {
            var row = $(this).closest('tr');
            var id = row.find('td:eq(0)').text();
            checkedPermissions.push(id);
        });

        // Enviar los datos al servicio web
        var ctrlActions = new ControlActions();
        var serviceCreate = this.UserApiService + "/RemovePermissions?userId=" + $("#userId").val();

        var bitacora = {};
        bitacora.Descripcion = "Se removieron permisos al usuario de ID: " + $("#userId").val();
        bitacora.UsuarioId = localStorage.getItem("idUsuario");
        bitacora.FechaRegistro = new Date();

        ctrlActions.PostToAPIv1(serviceCreate, checkedPermissions, function () {
            ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                self.CleanForm();
                Swal.fire({
                    title: 'Permisos removidos',
                    text: '¡Los permisos se han removido exitosamente!',
                    icon: 'success',
                    confirmButtonText: 'Entendido'
                });
            });
        });
    }

    this.LoadUsersTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.UserApiService + "/RetrieveAll")

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'primerApellido' }
        arrayColumnsData[3] = { 'data': 'correo' }

        $('#tblUsers').dataTable({
            "ajax": {
                "url": urlService,
                "dataSrc": ""
            },
            "columns": arrayColumnsData
        });

        $('#tblUsers tbody').on('click', 'tr', function () {

            var tr = $(this).closest('tr');
            var data = $('#tblUsers').DataTable().row(tr).data();

            $("#userId").val(data.id);
            $("#userName").val(data.nombre);

            $("#btnAssignPermissions").prop('disabled', false);
            $("#btnRemovePermissions").prop('disabled', false);
            $("#btnCancel").prop('disabled', false);

            self.LoadPermissionsTable();
        });
    }

    this.LoadPermissionsTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = "";
        var userId = $("#userId").val();

        if (userId === null && userId === "") {
            $("#tblPermissions").prop('hidden', true);
            urlService = ctrlActions.GetUrlApiService(this.UserApiService + "/GetUserPermissions");
        }
        else {
            $("#tblPermissions").prop('hidden', false);
            urlService = ctrlActions.GetUrlApiService(this.UserApiService + "/GetUserPermissions?userId=" + userId);
        }

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblPermissions')) {
            // Destruir la instancia existente
            $('#tblPermissions').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = {
            'data': null,
            'render': function (data, type, row) {
                console.log(data.estado);
                var checked = data.estado === "ACTIVO" ? 'checked' : '';
                return '<input type="checkbox" class="checkbox-delete" ' + checked + '>';
            }
        };

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
    }

    this.GoToPermissions = function () {
        window.location.href = "\ManagePermissions";
    }

    this.CleanForm = function () {
        $("#userId").val("");
        $("#userName").val("");
        $("#btnAssignPermissions").prop('disabled', true);
        $("#btnRemovePermissions").prop('disabled', true);
        $("#btnCancel").prop('disabled', true);
        self.LoadPermissionsTable();
    }
}

$(document).ready(function () {

    var view = new UsersPermissionsView();
    view.InitView();

})