function TendersManagementView() {

    this.ViewName = "TendersManagementView";
    this.ApiService = "Licitacion";
    this.UsuarioApiService = "Usuario";
    this.SchoolApiService = "Escuela";

    var self = this;

    this.InitView = function () {

        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new TendersManagementView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new TendersManagementView();
            view.Update();
        });

        $("#btnCancel").click(function () {
            var view = new TendersManagementView();
            view.CleanForm();
        });

        this.LoadProductsTable();
        this.LoadTendersTable();
        this.LoadUsers();
        this.LoadSchools();
        this.SetTenderData();
    }

    this.Create = function () {

        const licitacionEstado = document.getElementById("status");
        const fechaActual = new Date();
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

        const formValidation = this.InputsValidation($("#txtTitulo").val(), $("#txtDescripcion").val(), $("#txtPresupuesto").val(), checkedProducts.length);

        if (formValidation) {

            if (fechaActual < new Date($("#dateCierreOfertas").val()) && new Date($("#dateCierreOfertas").val()) < new Date($("#dateCierreLicitacion").val()) && new Date($("#dateCierreLicitacion").val()) < new Date($("#dateEntrega").val())) {

                var licitacion = {};

                licitacion.usuarioEncargado = $("#txtUsuarioEncargado").val();
                licitacion.escuela = $("#txtEscuela").val();
                licitacion.titulo = $("#txtTitulo").val();

                licitacion.descripcion = $("#txtDescripcion").val();
                licitacion.estado = "";
                licitacion.codigoQR = "";

                licitacion.presupuesto = $("#txtPresupuesto").val();
                licitacion.fechaCreacion = fechaActual;
                licitacion.fechaCierreOfertas = $("#dateCierreOfertas").val();

                licitacion.fechaCierreLicitacion = $("#dateCierreLicitacion").val();
                licitacion.fechaEntrega = $("#dateEntrega").val();
                licitacion.producto = checkedProducts;

                if (licitacionEstado.checked) {
                    licitacion.estado = "AUTOMATICA"
                } else {
                    licitacion.estado = "MANUAL"
                }

                var bitacora = {};
                bitacora.Descripcion = "Se creo una nueva licitación.";
                bitacora.UsuarioId = localStorage.getItem("idUsuario");
                bitacora.FechaRegistro = new Date();

                var ctrlActions = new ControlActions();
                var serviceCreate = this.ApiService + "/Create"

                ctrlActions.PostToAPIv1(serviceCreate, licitacion, function () {
                    ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                        self.CleanForm();
                        Swal.fire({
                            title: 'Licitación creada',
                            text: '¡La licitación se ha creado exitosamente!',
                            icon: 'success',
                            confirmButtonText: 'Entendido'
                        });
                    });
                });
            } else {
                Swal.fire({
                    title: 'Error con las fechas',
                    text: 'La fecha de entrega de la licitación debe ser mayor a la de cierre de licitación, la de cierre de licitación debe ser mayor a la de cierre de ofertas y de la ofertas debe ser mayor a la actual.',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                });
            }
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
        const licitacionEstado = document.getElementById("status");
        const fechaActual = new Date();
        var checkedProducts = [];

        $('#tblProducts tbody input[type="checkbox"]:checked').each(function () {
            var row = $(this).closest('tr');
            var id = row.find('td:eq(0)').text();
            var numberInput = row.find('td:eq(4) input[type="number"]').val(); // se agrega esta línea
            var product = {
                'id': id,
                'nombre': "",
                'unidadMedida': "",
                'cantidad': numberInput,
                'impuestos': []
            };
            checkedProducts.push(product);
        });

        $('#tblProducts tbody input[type="checkbox"]:not(:checked)').each(function () {
            var row = $(this).closest('tr');
            var id = row.find('td:eq(0)').text();
            var product = {
                'id': id,
                'nombre': "",
                'unidadMedida': "",
                'cantidad': 0,
                'impuestos': []
            };
            checkedProducts.push(product);
        });

        const formValidation = this.InputsValidation($("#txtTitulo").val(), $("#txtDescripcion").val(), $("#txtPresupuesto").val(), checkedProducts.length);

        if (formValidation) {

            if (new Date($("#dateCierreOfertas").val()) < new Date($("#dateCierreLicitacion").val()) && new Date($("#dateCierreLicitacion").val()) < new Date($("#dateEntrega").val())) {

                var licitacion = {};

                licitacion.Id = $("#txtId").val();
                licitacion.usuarioEncargado = $("#txtUsuarioEncargado").val();
                licitacion.escuela = $("#txtEscuela").val();
                licitacion.titulo = $("#txtTitulo").val();

                licitacion.descripcion = $("#txtDescripcion").val();
                licitacion.estado = "";
                licitacion.codigoQR = "";

                licitacion.presupuesto = $("#txtPresupuesto").val();
                licitacion.fechaCreacion = fechaActual;
                licitacion.fechaCierreOfertas = $("#dateCierreOfertas").val();

                licitacion.fechaCierreLicitacion = $("#dateCierreLicitacion").val();
                licitacion.fechaEntrega = $("#dateEntrega").val();
                licitacion.producto = checkedProducts;

                if (licitacionEstado.checked) {
                    licitacion.estado = "AUTOMATICA"
                } else {
                    licitacion.estado = "MANUAL"
                }

                var bitacora = {};
                bitacora.Descripcion = "Se modifico la licitación de ID: " + licitacion.Id;
                bitacora.UsuarioId = localStorage.getItem("idUsuario");
                bitacora.FechaRegistro = new Date();

                var ctrlActions = new ControlActions();
                var serviceCreate = this.ApiService + "/Update"

                ctrlActions.PutToAPI(serviceCreate, licitacion, function () {
                    ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                        self.CleanForm();
                        Swal.fire({
                            title: 'Licitación actualizada',
                            text: '¡La licitación se ha actualizado exitosamente!',
                            icon: 'success',
                            confirmButtonText: 'Entendido'
                        });
                    });
                });
            } else {
                Swal.fire({
                    title: 'Error con las fechas',
                    text: 'La fecha de entrega de la licitación debe ser mayor a la de cierre de licitación, la de cierre de licitación debe ser mayor a la de cierre de ofertas y de la ofertas debe ser mayor a la actual.',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                });
            }
        } else {
            Swal.fire({
                title: 'Error con el formulario',
                text: 'Por favor complete todos los campos del formulario.',
                icon: 'error',
                confirmButtonText: 'Entendido'
            });
        }

        
    }

    this.InputsValidation = (txtTitulo, txtDescripcion, txtPresupuesto, checkedProducts) => {
        if (txtTitulo === '' || txtDescripcion === '' || txtPresupuesto === '' || checkedProducts === 0) {

            return false;
        }
        return true;
    }

    this.LoadTendersTable = function () {
        const licitacionEstado = document.getElementById("status");

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAll");

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblTenders')) {
            // Destruir la instancia existente
            $('#tblTenders').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'titulo' }
        arrayColumnsData[2] = { 'data': 'presupuesto' }


        // Crear la instancia de la tabla tblPermissions con los datos vacíos
        var table = $('#tblTenders').DataTable({
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

        $('#tblTenders tbody').on('click', 'tr', function () {

            var tr = $(this).closest('tr');
            var data = $('#tblTenders').DataTable().row(tr).data();

            $("#txtId").val(data.id);
            $("#txtTitulo").val(data.titulo);
            $("#txtDescripcion").val(data.descripcion);
            $("#txtPresupuesto").val(data.presupuesto);
            $("#txtUsuarioEncargado").val(data.usuarioEncargado);
            $("#txtEscuela").val(data.escuela);

            if (data.estado === "AUTOMATICA") {
                licitacionEstado.checked = true;
            } else {
                licitacionEstado.checked = false;
            }

            $("#dateCierreOfertas").val(ConvertDate(data.fechaCierreOfertas));
            $("#dateCierreLicitacion").val(ConvertDate(data.fechaCierreLicitacion));
            $("#dateEntrega").val(ConvertDate(data.fechaEntrega));

            self.LoadProductsTable();

            $("#btnCreate").prop('disabled', true);
            $("#btnUpdate").prop('disabled', false);
        });
    }

    this.LoadProductsTable = function () {

        if ($("#txtId").val() === "" || $("#txtId").val() === null) {
            $("#txtId").val(0)
        }

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveAllLicitacionProducts?licitacionId=" + $("#txtId").val());

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
                return '<input type="number" class="form-control" value=' + quantity +'>';
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

    this.LoadUsers = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.UsuarioApiService + "/RetrieveAll");

        const select = document.getElementById("txtUsuarioEncargado");

        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                // Limpiamos el select antes de cargar los nuevos datos
                select.innerHTML = "";

                // Agregamos una opción por cada registro en la respuesta
                data.forEach((dato) => {
                    const option = document.createElement("option");
                    option.text = dato.nombre + " " + dato.primerApellido;
                    option.value = dato.id;
                    select.add(option);
                });
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    this.LoadSchools = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.SchoolApiService + "/RetrieveAll");

        const select = document.getElementById("txtEscuela");

        $.ajax({
            url: urlService,
            method: "GET",
            dataType: "json",
            success: function (data) {
                // Limpiamos el select antes de cargar los nuevos datos
                select.innerHTML = "";

                // Agregamos una opción por cada registro en la respuesta
                data.forEach((dato) => {
                    const option = document.createElement("option");
                    option.text = dato.nombre;
                    option.value = dato.id;
                    select.add(option);
                });
            },
            error: function (error) {
                console.log(error);
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

    this.CleanForm = function () {
        const licitacionEstado = document.getElementById("status");


        $("#txtId").val("");
        licitacionEstado.checked = false;
        $("#txtTitulo").val("");
        $("#txtDescripcion").val("");
        $("#txtPresupuesto").val("");
        $("#dateCierreOfertas").val("");
        $("#dateCierreLicitacion").val("");
        $("#dateEntrega").val("");

        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        self.LoadTendersTable()
        self.LoadProductsTable()
        self.LoadUsers();
        self.LoadSchools();
    }

    this.SetTenderData = function () {
        const licitacionEstado = document.getElementById("status");
        const urlParams = new URLSearchParams(window.location.search);
        const tenderId = urlParams.get('tenderId');

        if (tenderId !== null) {
            var ctrlActions = new ControlActions();
            var urlService = ctrlActions.GetUrlApiService(this.ApiService + "/RetrieveById?id=" + tenderId);

            $.ajax({
                url: urlService,
                method: "GET",
                dataType: "json",
                success: function (data) {
                    $("#txtId").val(data.id);
                    $("#txtTitulo").val(data.titulo);
                    $("#txtDescripcion").val(data.descripcion);
                    $("#txtPresupuesto").val(data.presupuesto);
                    $("#txtUsuarioEncargado").val(data.usuarioEncargado);
                    $("#txtEscuela").val(data.escuela);

                    if (data.estado === "AUTOMATICA") {
                        licitacionEstado.checked = true;
                    } else {
                        licitacionEstado.checked = false;
                    }

                    $("#dateCierreOfertas").val(ConvertDate(data.fechaCierreOfertas));
                    $("#dateCierreLicitacion").val(ConvertDate(data.fechaCierreLicitacion));
                    $("#dateEntrega").val(ConvertDate(data.fechaEntrega));

                    self.LoadProductsTable();

                    $("#btnCreate").prop('disabled', true);
                    $("#btnUpdate").prop('disabled', false);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    }
}

$(document).ready(function () {

    var view = new TendersManagementView();
    view.InitView();

})