function AssignBillsView() {

    this.ViewName = "AssignBillsView";
    this.ApiService = "Factura/";

    this.InitView = function () {

        this.ObtenerFacturas();

    }

    this.ObtenerFacturas = () => {

        this.facturas = [];

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetriveByUserReceiver?userId=" + localStorage.getItem("idUsuario"), (data) => {

            this.facturas = data;

            $("#viewAssignBills").empty();

            data.forEach(factura => {
                controlActions.GetToApiV2("Usuario/RetrieveById?id=" + factura.usuarioEmisor, (data) => {
                    const emisor = data;
                    controlActions.GetToApiV2("Usuario/RetrieveById?id=" + factura.usuarioReceptor, (data) => {
                        const receptor = data;
                        controlActions.GetToApiV2("Licitacion/RetrieveById?id=" + factura.licitacion, (data) => {
                            const licitacion = data;

                            $("#viewAssignBills").append(
                                `<div class="card mt-5" id="billCard-${factura.id}">
                                    <div class="card-body">
                                        <h3 class="card-title">Precio: ₡${factura.precio}</h3>
                                        <h4>Fecha de creacion: ${ConvertDate(factura.fechaCreacion)}</h4>
                                        <hr>
                                        <h4>Emisor:</h4>
                                        <h6>-Cédula:${emisor.cedula}</h6>
                                        <h6>-Nombre:${emisor.nombre} ${emisor.primerApellido} ${emisor.segundoApellido}</h6>
                                        <hr>
                                        <h4>Receptor:</h4>
                                        <h6>-Cédula:${receptor.cedula}</h6>
                                        <h6>-Nombre:${receptor.nombre} ${receptor.primerApellido} ${receptor.segundoApellido}</h6>
                                        <hr>
                                        <h4>Licitacion:</h4>
                                        <h6>-Id: ${factura.licitacion}</h6>
                                        <h6>-Titulo: ${licitacion.titulo}</h6>
                                        <h6>-Descripción: ${licitacion.descripcion}</h6>
                                    </div>
                                </div>`
                            );
                        });
                    });
                });
            });
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

    var view = new AssignBillsView();
    view.InitView();

})