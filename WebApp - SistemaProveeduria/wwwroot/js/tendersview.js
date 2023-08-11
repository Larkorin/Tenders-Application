function TendersView() {

    this.ViewName = "TendersView";
    this.ApiService = "Licitacion/";

	var self = this;

	this.InitView = function () {

        this.ObtenerLicitaciones();

	}

    this.ObtenerLicitaciones = () => {
        this.licitaciones = [];

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetrieveAll", (data) => {

            this.licitaciones = data;

            $("#viewTenders").empty();

            data.forEach(licitacion => {
                controlActions.GetToApiV2("Escuela/RetrieveById?id=" + licitacion.escuela, (data) => {
                    const escuela = data;

                    if (localStorage.getItem("idUsuario") !== null) {
                        if (localStorage.getItem("permisos").includes("A")) {
                            $("#viewTenders").append(
                                `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                <div class="card-body">
                                    <h3 class="card-title">${licitacion.titulo}</h3>
                                    <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
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
                                    <div class="tendersBtnsContainer">
                                        <a href="https://localhost:7071/ManageTenders?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnEditTender${licitacion.id}">Editar</a>
                                        <a href="https://localhost:7071/GenerateOffer?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnLetsOfferTender${licitacion.id}">Ofertar</a>
                                        <a href="https://localhost:7071/Offers?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnSeeOffersTender${licitacion.id}">Ver ofertas</a>
                                        <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
                                        <a href="https://localhost:7071/GenerateBill?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnBill${licitacion.id}">Facturar</a>
						            </div>
                                </div>
                            </div>`
                            );
                        } else if (localStorage.getItem("permisos").includes(2) || localStorage.getItem("permisos").includes(9) || localStorage.getItem("permisos").includes(10)) {
                            if (localStorage.getItem("permisos").includes(2) && localStorage.getItem("permisos").includes(9) && localStorage.getItem("permisos").includes(10)) {
                                $("#viewTenders").append(
                                    `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                        <div class="card-body">
                                            <h3 class="card-title">${licitacion.titulo}</h3>
                                            <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                            <h4>Beneficiario: ${escuela.nombre}</h2>
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
                                            <div class="tendersBtnsContainer">
                                                <a href="https://localhost:7071/ManageTenders?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnEditTender${licitacion.id}">Editar</a>
                                                <a href="https://localhost:7071/Offers?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnSeeOffersTender${licitacion.id}">Ver ofertas</a>
                                                <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
                                                <a href="https://localhost:7071/GenerateBill?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnBill${licitacion.id}">Facturar</a>
						                    </div>
                                        </div>
                                    </div>`
                                );
                            } else if (localStorage.getItem("permisos").includes(2) && localStorage.getItem("permisos").includes(9)) {
                                $("#viewTenders").append(
                                    `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                        <div class="card-body">
                                            <h3 class="card-title">${licitacion.titulo}</h3>
                                            <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                            <h4>Beneficiario: ${escuela.nombre}</h2>
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
                                            <div class="tendersBtnsContainer">
                                                <a href="https://localhost:7071/ManageTenders?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnEditTender${licitacion.id}">Editar</a>
                                                <a href="https://localhost:7071/Offers?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnSeeOffersTender${licitacion.id}">Ver ofertas</a>
                                                <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
						                    </div>
                                        </div>
                                    </div>`
                                );
                            } else if (localStorage.getItem("permisos").includes(2) && localStorage.getItem("permisos").includes(10)) {
                                $("#viewTenders").append(
                                    `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                        <div class="card-body">
                                            <h3 class="card-title">${licitacion.titulo}</h3>
                                            <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                            <h4>Beneficiario: ${escuela.nombre}</h2>
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
                                            <div class="tendersBtnsContainer">
                                                <a href="https://localhost:7071/ManageTenders?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnEditTender${licitacion.id}">Editar</a>
                                                <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
                                                <a href="https://localhost:7071/GenerateBill?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnBill${licitacion.id}">Facturar</a>
						                    </div>
                                        </div>
                                    </div>`
                                );
                            } else if (localStorage.getItem("permisos").includes(9) && localStorage.getItem("permisos").includes(10)) {
                                $("#viewTenders").append(
                                    `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                        <div class="card-body">
                                            <h3 class="card-title">${licitacion.titulo}</h3>
                                            <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                            <h4>Beneficiario: ${escuela.nombre}</h2>
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
                                            <div class="tendersBtnsContainer">
                                                <a href="https://localhost:7071/Offers?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnSeeOffersTender${licitacion.id}">Ver ofertas</a>
                                                <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
                                                <a href="https://localhost:7071/GenerateBill?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnBill${licitacion.id}">Facturar</a>
						                    </div>
                                        </div>
                                    </div>`
                                );
                            } else if (localStorage.getItem("permisos").includes(2)) {
                                $("#viewTenders").append(
                                    `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                        <div class="card-body">
                                            <h3 class="card-title">${licitacion.titulo}</h3>
                                            <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                            <h4>Beneficiario: ${escuela.nombre}</h2>
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
                                            <div class="tendersBtnsContainer">
                                                <a href="https://localhost:7071/ManageTenders?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnEditTender${licitacion.id}">Editar</a>
                                                <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
						                    </div>
                                        </div>
                                    </div>`
                                );
                            } else if (localStorage.getItem("permisos").includes(9)) {
                                $("#viewTenders").append(
                                    `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                        <div class="card-body">
                                            <h3 class="card-title">${licitacion.titulo}</h3>
                                            <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                            <h4>Beneficiario: ${escuela.nombre}</h2>
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
                                            <div class="tendersBtnsContainer">
                                                <a href="https://localhost:7071/Offers?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnSeeOffersTender${licitacion.id}">Ver ofertas</a>
                                                <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
						                    </div>
                                        </div>
                                    </div>`
                                );
                            } else if (localStorage.getItem("permisos").includes(10)) {
                                $("#viewTenders").append(
                                    `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                        <div class="card-body">
                                            <h3 class="card-title">${licitacion.titulo}</h3>
                                            <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                            <h4>Beneficiario: ${escuela.nombre}</h2>
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
                                            <div class="tendersBtnsContainer">
                                                <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
                                                <a href="https://localhost:7071/GenerateBill?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnBill${licitacion.id}">Facturar</a>
						                    </div>
                                        </div>
                                    </div>`
                                );
                            }
                        } else {
                            $("#viewTenders").append(
                                `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                    <div class="card-body">
                                        <h3 class="card-title">${licitacion.titulo}</h3>
                                        <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                        <h4>Beneficiario: ${escuela.nombre}</h2>
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
                                        <div class="tendersBtnsContainer">
                                            <a href="https://localhost:7071/GenerateOffer?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnLetsOfferTender${licitacion.id}">Ofertar</a>
                                            <a href="https://localhost:7071/TenderDetails?tenderId=${licitacion.id}" class="btn btn-primary bg-primario boton" id="btnTenderDetails${licitacion.id}">Detalles</a>
						                </div>
                                    </div>
                                </div>`
                            );
                        }     
                    } else {
                        $("#viewTenders").append(
                            `<div class="card mt-5" id="tenderCard-${licitacion.id}">
                                <div class="card-body">
                                    <h3 class="card-title">${licitacion.titulo}</h3>
                                    <h4>Presupuesto: ₡${licitacion.presupuesto}</h2>
                                    <h4>Beneficiario: ${escuela.nombre}</h2>
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
                            </div>`
                        );
                    }         
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

    var view = new TendersView();
    view.InitView();

})