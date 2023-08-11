function TaxesView() {

    this.ViewName = "TaxesView";
    this.ApiService = "Impuesto";

    var self = this;

    this.InitView = function () {

        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new TaxesView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new TaxesView();
            view.Update();
        });

        $("#btnCancel").click(function () {
            var view = new TaxesView();
            view.CleanForm();
        });

        this.LoadTable();
    }

    this.Create = function () {

        const formValidation = this.InputsValidation($("#txtTaxName").val(), $("#txtTaxValue").val());

        if (formValidation) {
            var tax = {};
            tax.Nombre = $("#txtTaxName").val();
            tax.ValorPorcentual = $("#txtTaxValue").val();

            var bitacora = {};
            bitacora.Descripcion = "Se creo un nuevo impuesto.";
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPIv1(serviceCreate, tax, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Impuesto creado',
                        text: '¡El impuesto se ha creado exitosamente!',
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

        const formValidation = this.InputsValidation($("#txtTaxName").val(), $("#txtTaxValue").val());

        if (formValidation) {
            var tax = {};
            tax.Id = $("#txtId").val();
            tax.Nombre = $("#txtTaxName").val();
            tax.ValorPorcentual = $("#txtTaxValue").val();

            var bitacora = {};
            bitacora.Descripcion = "Se modifico el impuesto de ID: " + tax.Id;
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, tax, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Impuesto actualizado',
                        text: '¡El impuesto se ha actualizado exitosamente!',
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

    this.InputsValidation = (txtNombreImpuesto, txtValor) => {
        if (txtNombreImpuesto === '' || txtValor === '') {

            return false;
        }
        return true;
    }

    this.LoadTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblTaxes')) {
            // Destruir la instancia existente
            $('#tblTaxes').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'valorPorcentual' }


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

        $('#tblTaxes tbody').on('click', 'tr', function () {

            var tr = $(this).closest('tr');
            var data = $('#tblTaxes').DataTable().row(tr).data();

            $("#txtId").val(data.id);
            $("#txtTaxName").val(data.nombre);
            $("#txtTaxValue").val(data.valorPorcentual);

            $("#btnCreate").prop('disabled', true);
            $("#btnUpdate").prop('disabled', false);
        });
    }

    this.CleanForm = function () {
        $("#txtId").val("")
        $("#txtTaxName").val("");
        $("#txtTaxValue").val("");
        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        self.LoadTable();
    }
}

$(document).ready(function () {

    var view = new TaxesView();
    view.InitView();

})