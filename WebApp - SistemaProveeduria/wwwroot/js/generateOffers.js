function GenerateOffersView() {

    this.ViewName = "GenerateOffersView";
    this.ApiService = "Oferta";
    this.ProductApiService = "Producto";

    var self = this;

    this.InitView = function () {

        $("#txtTender").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new GenerateOffersView();
            view.Create();
        });

        $("#btnCancel").click(function () {
            var view = new GenerateOffersView();
            view.CleanForm();
        });

        this.CheckTender();
        
    }

    this.Create = function () {
        var checkedProducts = [];

        $('#tblProducts tbody input[type="checkbox"]:checked').each(function () {
            var row = $(this).closest('tr');
            var id = row.find('td:eq(0)').text();
            var numberInput = row.find('td:eq(4) input[type="number"]').val();
            var product = {
                'id': id,
                'nombre': "",
                'unidadMedida': "",
                'cantidad': numberInput,
                'impuestos': []
            };
            checkedProducts.push(product);
        });

        const formValidation = this.InputsValidation($("#txtOfferPrice").val(), checkedProducts);

        if (formValidation) {

            var oferta = {};

            oferta.licitacion = $("#txtTender").val();
            oferta.oferente = localStorage.getItem('idUsuario');
            oferta.estado = "ACTIVA";
            oferta.precio = $("#txtOfferPrice").val();
            oferta.producto = checkedProducts;

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            var bitacora = {};
            bitacora.Descripcion = "Se creo una nueva oferta.";
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            ctrlActions.PostToAPIv1(serviceCreate, oferta, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Oferta creada',
                        text: '¡La oferta se ha creado exitosamente!',
                        icon: 'success',
                        confirmButtonText: 'Entendido'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location.href = "\Tenders";
                        }
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

    this.SetTenderId = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const tenderId = urlParams.get('tenderId');
        $("#txtTender").val(tenderId);    
    }

    this.LoadProductsTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ProductApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblProducts')) {
            // Destruir la instancia existente
            $('#tblProducts').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'unidadMedida' }
        arrayColumnsData[3] = {
            'data': null,
            'render': function (data, type, row) {
                var checked = data.cantidad > 0 ? 'checked' : '';
                return '<input type="checkbox" class="checkbox-delete" ' + checked + '>';
            }
        }
        arrayColumnsData[4] = {
            'data': null,
            'render': function (data, type, row) {
                var quantity = data.cantidad;
                return '<input type="number" class="form-control" value=' + quantity + '>';
            }
        }

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
    }

    this.InputsValidation = (txtOfferPrice, checkedProducts) => {
        if (txtOfferPrice === '' || checkedProducts === 0) {

            return false;
        }
        return true;
    }

    this.CleanForm = function () {

        $("#txtOfferPrice").val("");
    }

    this.CheckTender = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const tenderId = urlParams.get('tenderId');

        const controlActions = new ControlActions();
        controlActions.GetToApiV2("Licitacion/RetrieveById?id=" + tenderId, (data) => {

            if (ConvertDate(data.fechaCierreOfertas) < ConvertDate(new Date()) || data.usuarioProveedor !== -1 || data.estado === "AUTOMATICA") {

                $("#txtOfferPrice").prop('disabled', true);
                $("#btnCreate").prop('disabled', true);
                $("#btnCancel").prop('disabled', true);

                Swal.fire({
                    title: 'Solicitud denegada',
                    text: '¡No se aceptan ofertas para esta licitación!',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = "\Tenders";
                    }
                });
            } else {
                $("#txtOfferPrice").prop('disabled', false);
                $("#btnCreate").prop('disabled', false);
                $("#btnCancel").prop('disabled', false);
                this.SetTenderId();
                this.LoadProductsTable();
            }

        });
    }

    const ConvertDate = (date) => {
        var fecha = new Date(date);

        const year = fecha.getFullYear();
        const month = (fecha.getMonth() + 1).toString().padStart(2, '0');
        const day = fecha.getDate().toString().padStart(2, '0');
        const fechaInput = `${year}-${month}-${day}`;

        return fechaInput;
    }
}

$(document).ready(function () {

    var view = new GenerateOffersView();
    view.InitView();

})