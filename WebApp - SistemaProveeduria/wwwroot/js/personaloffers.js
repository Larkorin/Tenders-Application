function OffersView() {

    this.ViewName = "OffersView";
    this.ApiService = "Oferta/";

    this.InitView = function () {

        this.ObtenerOfertas();

    }

    this.ObtenerOfertas = () => {

        this.ofertas = [];

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetriveOffersByUserId?userId=" + localStorage.getItem("idUsuario"), (data) => {

            this.ofertas = data;

            $("#viewPersonalOffers").empty();

            data.forEach(oferta => {
                controlActions.GetToApiV2("Usuario/RetrieveById?id=" + oferta.oferente, (data) => {
                    const oferente = data;

                    $("#viewPersonalOffers").append(
                        `<div class="card mt-5" id="offersCard-${oferta.id}">
                            <div class="card-body">
                                <h2 class="card-title">Oferta para la licitación ${oferta.licitacion}</h2>
                                <h3 class="card-title">Precio: ${oferta.precio}</h3>
                                <h4>Oferente: ${oferente.nombre} ${oferente.primerApellido} ${oferente.segundoApellido}</h4>
                                <h4>Estado: ${oferta.estado}</h4>
                                <hr>
                                <div class="offersBtnsContainer">
                                    <a href="https://localhost:7071/PersonalOfferDetails?offerId=${oferta.id}&tenderId=${oferta.licitacion}" class="btn btn-primary bg-primario boton" id="btnOfferDetails${oferta.id}">Detalles</a>
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