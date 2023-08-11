function MembershipsView() {

    this.ViewName = "MembershipsView";
    this.ApiService = "Membresia";
    this.TaxApiService = "Impuesto";

    var self = this;

    this.InitView = function () {

        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new MembershipsView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new MembershipsView();
            view.Update();
        });

        $("#btnCancel").click(function () {
            var view = new MembershipsView();
            view.CleanForm();
        });

        this.LoadTaxesTable();
        this.LoadMembershipsTable();
    }

    this.Create = function () {

        var checkedTaxes = [];

        // Obtener los valores de id de las filas seleccionadas
        $('#tblTaxes tbody input[type="checkbox"]:checked').each(function () {
            var row = $(this).closest('tr');
            var id = row.find('td:eq(0)').text();
            checkedTaxes.push(id);
        });

        const formValidation = this.InputsValidation($("#txtMembershipName").val(), $("#txtMembershipPrice").val(), checkedTaxes.length);

        if (formValidation) {

            var membresia = {};
            membresia.Nombre = $("#txtMembershipName").val();
            membresia.Precio = $("#txtMembershipPrice").val();
            membresia.Impuestos = checkedTaxes;

            var bitacora = {};
            bitacora.Descripcion = "Se creo una nueva membresia.";
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPIv1(serviceCreate, membresia, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Membresía creada',
                        text: '¡La membresía se ha creado exitosamente!',
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

        const formValidation = this.InputsValidation($("#txtMembershipName").val(), $("#txtMembershipPrice").val(), 1);

        if (formValidation) {

            var checkedTaxes = [];
            var membresia = {};
            membresia.Id = $("#txtId").val();
            membresia.Nombre = $("#txtMembershipName").val();
            membresia.Precio = $("#txtMembershipPrice").val();
            membresia.Impuestos = checkedTaxes;

            var bitacora = {};
            bitacora.Descripcion = "Se modifico la membresia de ID: " + membresia.Id;
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"


            ctrlActions.PutToAPI(serviceCreate, membresia, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Membresía actualizada',
                        text: '¡La membresía se ha actualizado exitosamente!',
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

    this.InputsValidation = (txtName, txtPrice, checkedTaxes) => {
        if (txtName === '' || txtPrice === '' || checkedTaxes === 0) {

            return false;
        }
        return true;
    }

    this.LoadMembershipsTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblMemberships')) {
            // Destruir la instancia existente
            $('#tblMemberships').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'precio' }


        // Crear la instancia de la tabla tblPermissions con los datos vacíos
        var table = $('#tblMemberships').DataTable({
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

        $('#tblMemberships tbody').on('click', 'tr', function () {

            var tr = $(this).closest('tr');
            var data = $('#tblMemberships').DataTable().row(tr).data();

            $("#txtId").val(data.id);
            $("#txtMembershipName").val(data.nombre);
            $("#txtMembershipPrice").val(data.precio);

            $("#btnCreate").prop('disabled', true);
            $("#btnUpdate").prop('disabled', false);
        });
    }

    this.LoadTaxesTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.TaxApiService + "/RetrieveAll");


        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblTaxes')) {
            // Destruir la instancia existente
            $('#tblTaxes').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'valorPorcentual' }
        arrayColumnsData[3] = {
            'data': null,
            'render': function (data, type, row) {
                return '<input type="checkbox" class="checkbox-delete" >';
            }
        };

        // Crear la instancia de la tabla tblPermissions con los datos vacíos
        var table = $('#tblTaxes').DataTable({
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

    this.CleanForm = function () {
        $("#txtId").val("");
        $("#txtMembershipName").val("");
        $("#txtMembershipPrice").val("");
        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        self.LoadTaxesTable();
        self.LoadMembershipsTable();
    }
}

$(document).ready(function () {

    var view = new MembershipsView();
    view.InitView();

})