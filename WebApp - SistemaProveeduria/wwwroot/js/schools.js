function SchoolsView() {

    this.ViewName = "SchoolsView";
    this.ApiService = "Escuela";

    var self = this;

    this.InitView = function () {

        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new SchoolsView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new SchoolsView();
            view.Update();
        });

        $("#btnCancel").click(function () {
            var view = new SchoolsView();
            view.CleanForm();
        });

        this.LoadTable();
    }

    this.Create = function () {

        const formValidation = this.InputsValidation($("#txtSchoolName").val(), $("#txtSchoolLocation").val());

        if (formValidation) {

            var escuela = {};
            escuela.Nombre = $("#txtSchoolName").val();
            escuela.Ubicacion = $("#txtSchoolLocation").val();

            var bitacora = {};
            bitacora.Descripcion = "Se creo una nueva escuela.";
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPIv1(serviceCreate, escuela, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Escuela creada',
                        text: '¡La escuela se ha creado exitosamente!',
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

        const formValidation = this.InputsValidation($("#txtSchoolName").val(), $("#txtSchoolLocation").val());

        if (formValidation) {

            var escuela = {};
            escuela.Id = $("#txtId").val();
            escuela.Nombre = $("#txtSchoolName").val();
            escuela.Ubicacion = $("#txtSchoolLocation").val();

            var bitacora = {};
            bitacora.Descripcion = "Se modifico la escuela de ID: " + escuela.Id;
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, escuela, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Escuela actualizada',
                        text: '¡La escuela se ha actualizado exitosamente!',
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

    this.InputsValidation = (txtSchoolName, txtSchoolLocation) => {
        if (txtSchoolName === '' || txtSchoolLocation === '') {

            return false;
        }
        return true;
    }

    this.LoadTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblSchools')) {
            // Destruir la instancia existente
            $('#tblSchools').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'ubicacion' }


        // Crear la instancia de la tabla tblPermissions con los datos vacíos
        var table = $('#tblSchools').DataTable({
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

        $('#tblSchools tbody').on('click', 'tr', function () {

            var tr = $(this).closest('tr');
            var data = $('#tblSchools').DataTable().row(tr).data();

            $("#txtId").val(data.id);
            $("#txtSchoolName").val(data.nombre);
            $("#txtSchoolLocation").val(data.ubicacion);

            $("#btnCreate").prop('disabled', true);
            $("#btnUpdate").prop('disabled', false);
        });
    }

    this.CleanForm = function () {
        $("#txtId").val("");
        $("#txtSchoolName").val("");
        $("#txtSchoolLocation").val("");
        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        self.LoadTable();
    }
}

$(document).ready(function () {

    var view = new SchoolsView();
    view.InitView();

})