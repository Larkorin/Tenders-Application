function GenerateBillView() {

    this.ViewName = "GenerateBillView";
    this.ApiService = "Factura";

    var self = this;

    this.InitView = function () {

        $("#txtTender").prop('disabled', true);

        $("#btnCreate").click(function () {
            var view = new GenerateBillView();
            view.Create();
        });

        $("#btnCancel").click(function () {
            var view = new GenerateBillView();
            view.CleanForm();
        });

        this.CheckTender();
        this.SetTenderId();
    }

    this.Create = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const tenderId = urlParams.get('tenderId');

        const formValidation = this.InputsValidation($("#txtBillPrice").val());

        if (formValidation) {

            var factura = {};

            factura.Licitacion = $("#txtTender").val();
            factura.UsuarioEmisor = localStorage.getItem('idUsuario');
            factura.Precio = $("#txtBillPrice").val();
            factura.UsuarioReceptor = 0;
            factura.FechaCreacion = new Date();

            var ctrlActions = new ControlActions();
            var serviceCreate = this.ApiService + "/Create"

            var bitacora = {};
            bitacora.Descripcion = "Se creo una nueva factura para la licitación de ID " + tenderId;
            bitacora.UsuarioId = localStorage.getItem("idUsuario");
            bitacora.FechaRegistro = new Date();

            ctrlActions.PostToAPIv1(serviceCreate, factura, function () {
                ctrlActions.PostToAPIv1("Bitacora/Create", bitacora, function () {
                    self.CleanForm();
                    Swal.fire({
                        title: 'Factura creada',
                        text: '¡La factura se ha creado exitosamente!',
                        icon: 'success',
                        confirmButtonText: 'Entendido'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location.href = "\Tenders";
                        }
                    });
                });
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

    this.SetTenderId = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const tenderId = urlParams.get('tenderId');
        $("#txtTender").val(tenderId);
    }

    this.InputsValidation = (txtBillPrice) => {
        if (txtBillPrice === '') {

            return false;
        }
        return true;
    }

    this.CleanForm = function () {

        $("#txtOfferPrice").val("");
    }

    this.CheckTender = function () {
        const urlParams = new URLSearchParams(window.location.search);
        const tenderId = urlParams.get('tenderId');

        const controlActions = new ControlActions();
        controlActions.GetToApiV2("Licitacion/RetrieveById?id=" + tenderId, (data) => {

            if (data.usuarioProveedor === -1) {

                $("#txtBillPrice").prop('disabled', true);
                $("#btnCreate").prop('disabled', true);
                $("#btnCancel").prop('disabled', true);

                Swal.fire({
                    title: 'Licitación no adjudicada',
                    text: '¡Esta licitación no está adjudicada, no se puede facturar!',
                    icon: 'error',
                    confirmButtonText: 'Entendido'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = "\Tenders";
                    }
                });
            } else {
                $("#txtBillPrice").prop('disabled', false);
                $("#btnCreate").prop('disabled', false);
                $("#btnCancel").prop('disabled', false);
            }

        });
    }
}

$(document).ready(function () {

    var view = new GenerateBillView();
    view.InitView();

})