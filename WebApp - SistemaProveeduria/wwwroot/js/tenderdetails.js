function TenderDetails() {

    this.ViewName = "TenderDetails";
    this.ApiService = "Licitacion/";

    let tenderId;

    this.InitView = function () {

        const urlParams = new URLSearchParams(window.location.search);
        tenderId = urlParams.get('tenderId');

        this.ObtenerDetalles();

        $("#btnGenerateOffer").click(function () {
            var view = new TenderDetails();
            view.GoToOffer(tenderId);
        });
    }

    this.ObtenerDetalles = () => {

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetrieveById?id=" + tenderId, (data) => {

            const licitacion = data;

            $("#viewTenderDetails").empty();

            controlActions.GetToApiV2("Escuela/RetrieveById?id=" + licitacion.escuela, (data) => {
                const escuela = data;

                if (licitacion.usuarioProveedor !== -1) {
                    controlActions.GetToApiV2("Usuario/RetrieveById?id=" + licitacion.usuarioProveedor, (data) => {
                        const proveedor = data;
                        $("#viewTenderDetails").append(
                            `<div class="mt-5" id="tenderCard-${licitacion.id}">
                                <div class="card-body">
                                    <h3 class="card-title">Título: ${licitacion.titulo}</h3>
                                    <h4 class="mt-3">Presupuesto: ₡${licitacion.presupuesto}</h2>
                                    <h4>Beneficiario: Escuela ${escuela.nombre}</h2>
                                    <hr>
                                    <div>
                                        <h5>Descripcion:</h5>
                                        <p>${licitacion.descripcion}</p>
                                    </div>
                                    <hr>
                                    <div class="flex-body">
                                        <h5>Datos generales:</h6>
                                        <div class="mt-4">
                                            <p><span class="h6">- Estado:</span> ${licitacion.estado}</p>
                                            <p><span class="h6">- Fecha de creación:</span> ${ConvertDate(licitacion.fechaCreacion)}</p>
                                            <p><span class="h6">- Fecha de cierre de ofertas:</span> ${ConvertDate(licitacion.fechaCierreOfertas)}</p>
                                            <p><span class="h6">- Fecha de cierre de licitación:</span> ${ConvertDate(licitacion.fechaCierreLicitacion)}</p>
                                            <p><span class="h6">- Fecha de entrega:</span> ${ConvertDate(licitacion.fechaEntrega)}</p>
                                        </div>
                                    </div>
                                    <hr>
                                    <div class="flex-body">
                                        <h5>Datos proveedor:</h6>
                                        <div class="mt-4">
                                            <p><span class="h6">- Cédula:</span> ${proveedor.cedula}</p>
                                            <p><span class="h6">- Nombre:</span> ${proveedor.nombre} ${proveedor.primerApellido} ${proveedor.segundoApellido}</p>
                                            <p><span class="h6">- Correo:</span> ${proveedor.correo}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr>`
                        );
                    });

                } else {
                    $("#viewTenderDetails").append(
                        `<div class="mt-5" id="tenderCard-${licitacion.id}">
                            <div class="card-body">
                                <h3 class="card-title">Título: ${licitacion.titulo}</h3>
                                <h4 class="mt-3">Presupuesto: ₡${licitacion.presupuesto}</h2>
                                <h4>Beneficiario: Escuela ${escuela.nombre}</h2>
                                <hr>
                                <div>
                                    <h5>Descripcion:</h5>
                                    <p>${licitacion.descripcion}</p>
                                </div>
                                <hr>
                                <div class="flex-body">
                                    <h5>Datos generales:</h6>
                                    <div class="mt-4">
                                        <p><span class="h6">- Estado:</span> ${licitacion.estado}</p>
                                        <p><span class="h6">- Fecha de creación:</span> ${ConvertDate(licitacion.fechaCreacion)}</p>
                                        <p><span class="h6">- Fecha de cierre de ofertas:</span> ${ConvertDate(licitacion.fechaCierreOfertas)}</p>
                                        <p><span class="h6">- Fecha de cierre de licitación:</span> ${ConvertDate(licitacion.fechaCierreLicitacion)}</p>
                                        <p><span class="h6">- Fecha de entrega:</span> ${ConvertDate(licitacion.fechaEntrega)}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr>`
                    );
                }

            });

            this.ObtenerProductos(tenderId);
        });
    }

    this.ObtenerProductos = (tenderId) => {

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetrieveAllLicitacionProducts?licitacionId=" + tenderId, (data) => {
            $("#viewTenderDetails").append(
                `<h3 class="mt-5">Productos:</h3>`
            )

            data.forEach(product => {
                if (product.cantidad > 0) {
                    $("#viewTenderDetails").append(
                        `<div class="mt-3">
                        <h4>Producto: ${product.nombre}</h4>
                        <h4>Cantidad: ${product.cantidad}</h4>
                        <hr>
				    </div>`
                    );
                }
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

    this.GoToOffer = (tenderId) => {
        window.location.href = "https://localhost:7071/GenerateOffer?tenderId=" + tenderId;
    }
}

$(document).ready(function () {

    var view = new TenderDetails();
    view.InitView();

})    