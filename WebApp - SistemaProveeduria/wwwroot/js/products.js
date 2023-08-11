function ProductsView() {

    this.ViewName = "ProductsView";
    this.ApiService = "Producto";
    this.TaxApiService = "Impuesto";

    var self = this;

    this.InitView = function () {

        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new ProductsView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new ProductsView();
            view.Update();
        });

        $("#btnCancel").click(function () {
            var view = new ProductsView();
            view.CleanForm();
        });

        this.LoadProductsTable();
        this.LoadTaxesTable();
    }

    this.Create = function () {

        var checkedTaxes = [];

        // Obtener los valores de id de las filas seleccionadas
        $('#tblTaxes tbody input[type="checkbox"]:checked').each(function () {
            var row = $(this).closest('tr');
            var id = row.find('td:eq(0)').text();
            checkedTaxes.push(id);
        });

        const formValidation = this.InputsValidation($("#txtProductName").val(), checkedTaxes.length);

        if (formValidation) {

            var product = {};
            product.Nombre = $("#txtProductName").val();
            product.UnidadMedida = $("#txtProductMeasurement").val();
            product.Impuestos = checkedTaxes;

            var bitacora = {};
            bitacora.Descripcion = "Se creo un nuevo producto";
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            ctrlActions.PostToAPIv1(serviceCreate, product, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Producto creado',
                        text: '¡El producto se ha creado exitosamente!',
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

        const formValidation = this.InputsValidation($("#txtProductName").val(), 1);

        if (formValidation) {

            var checkedTaxes = [];
            var product = {};
            product.Id = $("#txtId").val();
            product.Nombre = $("#txtProductName").val();
            product.UnidadMedida = $("#txtProductMeasurement").val();
            product.Impuestos = checkedTaxes;

            var bitacora = {};
            bitacora.Descripcion = "Se modifico el producto de ID: " + product.Id;
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Update"

            ctrlActions.PutToAPI(serviceCreate, product, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Producto actualizado',
                        text: '¡El producto se ha actualizado exitosamente!',
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

    this.InputsValidation = (txtNombreProducto, checkedTaxes) => {
        if (txtNombreProducto === '' || checkedTaxes === 0) {

            return false;
        }
        return true;
    }

    this.LoadProductsTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblProducts')) {
            // Destruir la instancia existente
            $('#tblProducts').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'unidadMedida' }


        // Crear la instancia de la tabla tblPermissions con los datos vacíos
        var table = $('#tblProducts').DataTable({
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

        $('#tblProducts tbody').on('click', 'tr', function () {

            var tr = $(this).closest('tr');
            var data = $('#tblProducts').DataTable().row(tr).data();

            $("#txtId").val(data.id);
            $("#txtProductName").val(data.nombre);
            $("#txtProductMeasurement").val(data.unidadMedida);

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
        $("#txtProductName").val("");
        $("#txtProductMeasurement").val("");
        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        self.LoadProductsTable();
        self.LoadTaxesTable();
    }
}

$(document).ready(function () {

    var view = new ProductsView();
    view.InitView();

})