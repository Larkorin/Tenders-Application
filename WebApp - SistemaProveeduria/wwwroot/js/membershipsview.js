function MembershipsView() {

    this.ViewName = "MembershipsView";
    this.ApiService = "Membresia/";
    this.ApiService2 = "Suscripcion/";
    this.InventarioService = "Inventario";
    this.membershipCost = "";
    this.processedMembership = {};
    this.paypalEmail = "";


    this.InitView = function () {

        this.validarSuscripcion();
        this.ObtenerMembresias();
        
    }

    this.ObtenerMembresias = () => {
        this.membresias = [];

        const controlActions = new ControlActions();
        controlActions.GetToApiV2(this.ApiService + "RetrieveAll", async (data) => {

            this.membresias = data;
            let index = 0;
            const buttons = new Array(data.length)
            let selectedMembership = {};

            $("#viewMemberships").empty();
            data.forEach(membresia => {
                $("#viewMemberships").append(
                    `<div class="card mt-5" id="membershipCard-${membresia.id}">
                        <div class="card-body">
                            <h3 class="card-title">${membresia.nombre}</h3>
                            <h5>Presupuesto: ₡${membresia.precio}</h5>
                            <hr>
                            <div class="mt-4">
                                <button value="${membresia.nombre}" type="button" class="btn btn-primary bg-primario boton" data-form-type="action" id="btnSubscribe${membresia.id}">Suscribirse</button>
                            </div>
                        </div>
                    </div>`
                );
                buttons[index] = '#btnSubscribe' + membresia.id;
                console.log(buttons[index])
                index++;
            });


            for (let i = 0; i < buttons.length; i++) {
                await $(buttons[i]).click(() => {
                    membershipID = buttons[i][buttons[i].length - 1]
                    data.forEach(membership => {
                        if (membershipID == membership.id) {
                            selectedMembership.id = membership.id;
                            selectedMembership.nombre = membership.nombre;
                            selectedMembership.precio = membership.precio;
                        }
                    })
                    $('#selectedName').text(selectedMembership.nombre);
                    $('#selectedPrice').text("₡" + selectedMembership.precio);
                    membershipCost = (selectedMembership.precio / 532).toFixed(2);
                    console.log("Nuevo costo de membresía: " + membershipCost);
                    processedMembership = selectedMembership;
                });
            }
        });

                    procesarPago();
        
    }

    this.validarSuscripcion = () => {
        let validacion = null;

        const obtenerUsuario = parseInt(localStorage.getItem("idUsuario"));

        console.log(obtenerUsuario)

        const controlActions = new ControlActions();
        const service = "Suscripcion/RetrieveById";
        const service2 = "Suscripcion/RetrieveById?id=" + obtenerUsuario;

        var datos = {};

       // controlActions.PostToAPIv1(service2, datos, (data, response) => {
         //   console.log(JSON.stringify(data))
        //});

        $.ajax({
            type: "GET",
            url: "https://localhost:7123/api/Suscripcion/RetrieveById?id=" + obtenerUsuario,
            xhrFields: {
                withCredentials: true
            },
            success: function (response) {
                validacion = (response);

            },
            error: function (xhr, status, error) {
                console.log("ERROR: " + error);
            }
        }).then(() => {
            console.log(validacion.membresia.nombre);
            if (validacion != null) {
                $("#viewMemberships").remove();
                $("#paypal-button-container").remove();
                $("#membershipTitle").text("Ya cuentas con una suscripción activa:");
                $('#selectedMembership').remove();
                $('#nombreMembresiaActiva').desabled = false;
                $('#precioMembresiaActiva').desabled = false;
                $('#fechaSuscripcionMembresiaActiva').desabled = false;
                $('#fechaSuscripcionMembresiaActiva').desabled = false;
                $('#nombreMembresiaActiva').text(validacion.membresia.nombre)
                $('#precioMembresiaActiva').text(validacion.membresia.precio)
                $('#fechaSuscripcionMembresiaActiva').text("Fecha de suscripción: " + validacion.fechaSuscripcion)
                $('#paypalMembresiaActiva').text("Cuenta de Paypal: " + validacion.correoPayPal)
            }
        });


    }
}


this.procesarPago = () => {
    const fundingSources = [
        paypal.FUNDING.PAYPAL
    ];

    var ctrlActions = new ControlActions();
    var serviceCreateInventory = "Inventario/Create?id=" + localStorage.getItem("idUsuario");
    var data3 = {};

    for (const fundingSource of fundingSources) {

        paypal
            .Buttons({
                fundingSource,

                style: {
                    layout: 'vertical',
                    shape: 'rect'
                },

                createOrder: function (data, actions) {
                    const createOrderPayload = {
                        purchase_units: [
                            {
                                amount: {
                                    value: membershipCost,
                                },
                            },
                        ],
                    };

                    return actions.order.create(createOrderPayload);
                },

                onApprove: function (data, actions, details) {
                    //codigo de pruebas
                    actions.order.capture().then( function (details) {
                        // Captura la transacción y obtiene los detalles del pago

                        // Accede al correo electrónico del comprador
                        
                        paypalEmail = details.payer.email_address;

                    }).then(() => {
                        const id = localStorage.getItem("idUsuario");
                        let subscription = {
                            "id": 0,
                            "usuarioSuscrito": {
                                "id": id,
                                "cedula": "string",
                                "nombre": "string",
                                "primerApellido": "string",
                                "segundoApellido": "string",
                                "correo": "string",
                                "telefono": "string",
                                "estado": "string",
                                "fechaRegistro": new Date().toJSON(),
                                "permisos": [],
                                "contrasenas": []
                            },
                            "membresia": {
                                "id": processedMembership.id,
                                "impuestos": [
                                    0
                                ],
                                "nombre": processedMembership.nombre,
                                "precio": processedMembership.precio
                            },
                            "fechaSuscripcion": new Date().toJSON(),
                            "correoPayPal": paypalEmail,
                            "estado": "Activo"
                        }

                        var ctrlActions = new ControlActions();
                        const serviceCreate = "Suscripcion" + "/Create"

                        ctrlActions.PostToAPIv1(serviceCreate, subscription, () => {
                            Swal.fire({
                                title: 'Suscripción completada con éxito!',
                                text: '¡Haz logrado suscribirte con éxito!',
                                icon: 'success',
                                confirmButtonText: 'Entendido'
                            });
                        })

                        //Hacer aqui el llamado del API de inventario
                        ctrlActions.PostToAPIv1(serviceCreateInventory, data3, function () {
                        });

                        location.reload();

                    })


                    
                },
            })
            .render("#paypal-button-container");
    }
}



$(document).ready(function () {

    var view = new MembershipsView();
    view.InitView();

})