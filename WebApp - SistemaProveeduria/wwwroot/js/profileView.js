function ProfileView() {

	this.ViewName = "ProfileView";
    this.ApiService = "Usuario/";

    var self = this;

    this.InitView = function () {

		this.ObtenerUsuario();
    }

	this.ObtenerUsuario = () => {
		this.usuario = {};

		const controlActions = new ControlActions();

		controlActions.GetToApi(this.ApiService + "RetrieveById?id=" + localStorage.getItem("idUsuario"), (data) => {

			usuario = data;
			console.log(usuario)
			console.log(usuario.nombre)

			$("#viewProfile").empty();

			$("#viewProfile").append(
			`<div class="card mt-5" id="userProfileCard">
				<div class="card-body">
					<h3 class="card-title">${usuario.nombre + " " + usuario.primerApellido + " " + usuario.segundoApellido}</h3>
					<hr>
					<div class="flex-body">
						<div>
							<p><span class="h6">Cédula:</span> ${usuario.cedula}</p>
							<p><span class="h6">Correo:</span> ${usuario.correo}</p>
							<p><span class="h6">Teléfono:</span> ${usuario.telefono}</p>
						</div>
					</div>
				</div>
			</div>
			<div class="btnsContainer mt-3">
                <a href="https://localhost:7071/BitacorasView" class="btn btn-primary bg-primario boton" id="btnViewBitacoras">Mis Bitácoras</a>
				<a href="https://localhost:7071/PersonalBills" class="btn btn-primary bg-primario boton" id="btnViewBills">Mis Facturas</a>
				<a href="https://localhost:7071/PersonalOffers" class="btn btn-primary bg-primario boton" id="btnViewOffers">Mis Ofertas</a>
			</div>`
			);
		});
    }

}
$(document).ready(function () {

	var view = new ProfileView();
    view.InitView();

})