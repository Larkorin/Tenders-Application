function OffersView() {

    this.ViewName = "OffersView";
    this.ApiService = "Oferta/";

    this.InitView = function () {

        this.ObtenerOfertas();

    }

    this.ObtenerOfertas = () => {
        const urlParams = new URLSearchParams(window.location.search);
        const tenderId = urlParams.get('tenderId');

        this.ofertas = [];

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetrieveAllOffersByTenderId?tenderId=" + tenderId, (data) => {

            this.ofertas = data;

            $("#viewOffers").empty();

            $("#viewOffers").append(
                `<h2 class="mt-3">Ofertas para la licitación ${tenderId}</h2>`
            );

            data.forEach(oferta => {
                controlActions.GetToApiV2("Usuario/RetrieveById?id=" + oferta.oferente, (data) => {
                    const oferente = data;

                    $("#viewOffers").append(
                        `<div class="card mt-5" id="offersCard-${oferta.id}">
                            <div class="card-body">
                                <h3 class="card-title">Precio: ${oferta.precio}</h3>
                                <h4>Oferente: ${oferente.nombre} ${oferente.primerApellido} ${oferente.segundoApellido}</h4>
                                <h4>Estado: ${oferta.estado}</h4>
                                <hr>
                                <div class="offersBtnsContainer">
                                    <a href="https://localhost:7071/OfferDetails?offerId=${oferta.id}&tenderId=${tenderId}" class="btn btn-primary bg-primario boton" id="btnOfferDetails${oferta.id}">Detalles</a>
						        </div>
                            </div>
                        </div>`
                    );
                });
            });
        });
    }
}

$(document).ready(function () {

    var view = new OffersView();
    view.InitView();

})