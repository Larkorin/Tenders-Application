function InventoryView() {

this.ViewName = "InventoryView";
this.ProductoApiServer = "Producto";
this.InventarioApiServer = "Inventario";

var self = this;

    this.InitView = function () {
        this.FindInventory();
        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new InventoryView();
            view.Create();
        });

        $("#btnUpdate").click(function () {
            var view = new InventoryView();
            view.Update();
        });

        $("#btnCancel").click(function () {
            var view = new InventoryView();
            view.CleanForm();
        });

        this.LoadProductsTable();   
        this.LoadProducts();

    }

    this.Create = function () {
        const fechaActual = new Date();

        if ($("#txtProduct").val() !== "" && $("#txtInventoryQuantity").val() !== "" && $("#txtInventoryPrice").val() !== "") {

            var data = $('#tblInventory').DataTable().row(this).data();
            //$("#txtProduct").val(data.Producto);
            //$("#txtInventoryQuantity").val(data.Cantidad);
            //$("#txtInventoryPrice").val(data.Precio);

            $("#txtInventoryUpdateDate").val(fechaActual.toLocaleDateString());

            //$("#txtId").val(data.id);
            //$("#txtProducto").val(data.nombre);
            //$("#txtInventoryQuantity").val(data.cantidad);
            //$("#txtInventoryPrice").val(data.precio);
            var ctrlActions = new ControlActions();
            //idinventario
            var serviceCreate = this.InventarioApiServer + "/AssignProduct?idInventory=" + localStorage.getItem("idInventario") + "&idProduct=" + $("#txtProducto").val() + "&quantity=" + $("#txtInventoryQuantity").val() + "&price=" + $("#txtInventoryPrice").val();
            var data2 = {};
            
            ctrlActions.PostToAPIv1(serviceCreate, data2, function () {
                Swal.fire({
                    title: 'Producto Añadido',
                    text: "¡Se ha añadido el producto!",
                    icon: 'success',
                    confirmButtonText: 'Entendido'
                });
                self.CleanForm();
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

        if ($("#txtProduct").val() !== "" && $("#txtInventoryQuantity").val() !== "" && $("#txtInventoryPrice").val() !== "") {

            var data = $('#tblInventory').DataTable().row(this).data();


            var ctrlActions = new ControlActions();
            var serviceUpdate = this.InventarioApiServer + "/UpdateProductInventario?idInventory=" + localStorage.getItem("idInventario") + "&idProduct=" + $("#txtId").val() + "&quantity=" + $("#txtInventoryQuantity").val() + "&price=" + $("#txtInventoryPrice").val(); 
            var data2 = {};

            ctrlActions.PutToAPI(serviceUpdate, data2, function () {
                Swal.fire({
                    title: 'Actualizacion Exitosa',
                    text: "¡Se ha actualizado el producto!",
                    icon: 'success',
                    confirmButtonText: 'Entendido'
                });
                self.CleanForm();
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
    this.FindInventory = function () {

        var ctrlActions = new ControlActions();
        var urlService = this.InventarioApiServer + "/RetrieveByUserId?id=" + localStorage.getItem("idUsuario");

        ctrlActions.GetToApi(urlService, function (response) {
            if (response !== null && response !== undefined) {
                //console.log(response.id);
                localStorage.setItem("idInventario", response.id);
            }
        })
    }

    this.LoadProducts = function () {
        var ctrlActions = new ControlActions();
        var service = ctrlActions.GetUrlApiService(this.ProductoApiServer + "/RetrieveAll");

        const select = document.getElementById("txtProducto");

        $.ajax({
            url: service,
            method: "GET",
            dataType: "json",
            success: function (data) {
                select.innerHTML = "";

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

    this.LoadProductsTable = function () {

        var ctrlActions = new ControlActions();
        var urlService = ctrlActions.GetUrlApiService(this.InventarioApiServer + "/RetrieveAllProductsInventario?idInventario=" + + localStorage.getItem("idInventario"));

        // Verificar si ya existe una instancia de la tabla tblPermissions
        if ($.fn.DataTable.isDataTable('#tblInventory')) {
            // Destruir la instancia existente
            $('#tblInventory').DataTable().destroy();
        }

        var arrayColumnsData = [];
        arrayColumnsData[0] = { 'data': 'id' }
        arrayColumnsData[1] = { 'data': 'nombre' }
        arrayColumnsData[2] = { 'data': 'precio' }
        arrayColumnsData[3] = { 'data': 'cantidad' }
        arrayColumnsData[4] = { 'data': 'fechaRegistro' }



        // Crear la instancia de la tabla tblPermissions con los datos vacíos
        var table = $('#tblInventory').DataTable({
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
                console.log(data);
            },
            error: function (error) {
                console.log(error);
            }
        });

        $('#tblInventory tbody').on('click', 'tr', function () {

            var tr = $(this).closest('tr');
            var data = $('#tblInventory').DataTable().row(tr).data();

            ////////
            //console.log(data);
            //var nombre = console.log("data=" +  data.nombre);
            //console.log(data.id);
            //console.log("nombre " + data.nombre);
            ////////

            $("#txtId").val(data.id);
            $("#txtProducto").val(data.id);
            $("#txtInventoryQuantity").val(data.cantidad);
            $("#txtInventoryPrice").val(data.precio);

            $("#btnCreate").prop('disabled', true);
            $("#btnUpdate").prop('disabled', false);
        });
    }

    this.CleanForm = function () {
        $("#txtId").val("");
        $("#txtProducto").val("");
        $("#txtInventoryQuantity").val("");
        $("#txtInventoryPrice").val("");

        $("#btnCreate").prop('disabled', false);
        $("#btnUpdate").prop('disabled', true);

        self.LoadProducts();
        self.LoadProductsTable();
    }

}


$(document).ready(function () {

var view = new InventoryView();
view.InitView();

})