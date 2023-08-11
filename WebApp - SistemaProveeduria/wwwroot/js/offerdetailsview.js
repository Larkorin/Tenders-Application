function OfferDetailsView() {

    this.ViewName = "OfferDetailsView";
    this.ApiService = "Oferta/";

    let tenderId;
    let offerId;

    this.InitView = function () {

        const urlParams = new URLSearchParams(window.location.search);
        offerId = urlParams.get('offerId');
        tenderId = urlParams.get('tenderId');

        this.ObtenerDetallesOferta();

        $("#btnAdjudicar").click(function () {
            var view = new OfferDetailsView();
            view.Adjudicar(offerId, tenderId);
        });

        $("#btnGoBack").click(function () {
            var view = new OfferDetailsView();
            view.GoBack(tenderId);
        });
    }

    this.ObtenerDetallesOferta = () => {

        console.log(offerId);
        console.log(tenderId);

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetrieveById?id=" + offerId, (data) => {

            const oferta = data;
            licitacionId = oferta.licitacion;

            $("#offerDetails").empty();

            controlActions.GetToApiV2("Usuario/RetrieveById?id=" + oferta.oferente, (data) => {
                const oferente = data;

                $("#offerDetails").append(
                    `<div class="mt-5" id="offerCard-${offerId}">
                        <div class="card-body">
                            <h3 class="card-title">Precio: ${oferta.precio}</h3>
                            <h4 class="mt-3">Oferente: ${oferente.nombre} ${oferente.primerApellido} ${oferente.segundoApellido}</h2>
                            <h4>Estado: ${oferta.estado}</h4>
                            <hr>
                        </div>
                    </div>`  
                );
            });

            this.ObtenerProductos(offerId); 
        });
    }

    this.ObtenerProductos = (offerId) => {

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetriveProductsByOfferId?offerId=" + offerId, (data) => {
            $("#offerDetails").append(
                `<h3 class="mt-5">Productos:</h3>`
            )

            data.forEach(product => {
                $("#offerDetails").append(
                    `<div class="mt-3">
                        <h4>Producto: ${product.nombre}</h4>
                        <h4>Cantidad: ${product.cantidad}</h4>
                        <hr>
				    </div>`
                );
            });
        });
    }

    this.GoBack = (tenderId) => {
        window.location.href = "https://localhost:7071/Offers?tenderId=" + tenderId;
    }

    this.Adjudicar = (offerId, tenderId) => {

        const controlActions = new ControlActions();
        controlActions.GetToApiV2("Licitacion/RetrieveById?id=" + tenderId, (data) => {
            if (data.usuarioProveedor === -1) {
                $.ajax({
                    type: "POST",
                    url: "https://localhost:7123/api/Licitacion/AdjudicarLicitacionOferta?licitacionId=" + tenderId + "&ofertaId=" + offerId,
                    xhrFields: {
                        withCredentials: true
                    },
                    success: function () {
                        Swal.fire({
                            title: 'Oferta adjudicada',
                            text: '¡La oferta se ha adjudicado exitosamente!',
                            icon: 'success',
                            confirmButtonText: 'Entendido'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.href = "\Tenders";
                            }
                        });
                    },
                    error: function (xhr, status, error) {
                        console.log("ERROR: " + error);
                    }
                });
            } else {
                Swal.fire({
                    title: 'Solicitud denegada',
                    text: '¡Esta licitación ya está adjudicada, no se puede volver a adjudicar!',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                });
            }
        });
    }
}

$(document).ready(function () {

    var view = new OfferDetailsView();
    view.InitView();

})    