function BitacorasView() {

    this.ViewName = "BitacorasView";
    this.ApiService = "Bitacora/";

    var self = this;

    this.InitView = function () {

        this.ObtenerBitacoras();

    }

    this.ObtenerBitacoras = () => {
        this.bitacoras = [];

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetrieveByUserId?userId=" + localStorage.getItem('idUsuario'), (data) => {

            this.bitacoras = data;

            $("#viewBitacoras").empty();

            data.forEach(bitacora => {
                $("#viewBitacoras").append(
                    `<div class="card mt-5" id="bitacoraCard-${bitacora.id}">
                        <div class="card-body">
                            <h3>Fecha de registro:</h3>
                            <h4>${ConvertDate(bitacora.fechaRegistro)}</h4>
                            <hr>
                            <h5>Descripcion:</h5>
                            <p>${bitacora.descripcion}</p>
                        </div>
                    </div>`
                );
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

    var view = new BitacorasView();
    view.InitView();

})