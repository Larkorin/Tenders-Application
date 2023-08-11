function IndexView() {

    this.ViewName = "IndexView";
    var self = this;

    this.InitView = function () {

        this.CheckLocalStorage();
        this.CheckPremium();

        $('#logOut').click(() => {
            var view = new IndexView();
            view.LogOut();
        });
    }

    this.CheckLocalStorage = function () {

        var usernameInput = document.getElementById("usernameNav");   
        var username = localStorage.getItem("username")

        if (localStorage.getItem("idUsuario") !== null) {
            $("#regularNav").prop('hidden', true);
            $("#sessionNav").prop('hidden', false);
            this.CheckUserPermissions();
            console.log(username);  
            usernameInput.textContent = username;

        } else {
            $("#regularNav").prop('hidden', false);
            $("#sessionNav").prop('hidden', true);
            $("#usernameNav").val("");
        }
    }

    this.CheckUserPermissions = function () {

        var permissions = [];
        permissions = localStorage.getItem("permisos")

        if (permissions.includes("A")) {
            $("#crudOptions").prop('hidden', false);
            $("#billsOption").prop('hidden', false);
            $("#permissionsOption").prop('hidden', false);
            $("#taxesOption").prop('hidden', false);
            $("#productsOption").prop('hidden', false);
            $("#schoolsOption").prop('hidden', false);
            $("#membershipsOption").prop('hidden', false);
            $("#tenderssOption").prop('hidden', false);

        } else if (permissions.includes(2) || permissions.includes(4) || permissions.includes(5) || permissions.includes(6) || permissions.includes(7) || permissions.includes(8)) {
            
            $("#crudOptions").prop('hidden', false);

            if (permissions.includes(2)) {
                $("#tenderssOption").prop('hidden', false);
            } else {
                $("#tenderssOption").prop('hidden', true);
            }

            if (permissions.includes(4)) {
                $("#membershipsOption").prop('hidden', false);
            } else {
                $("#membershipsOption").prop('hidden', true);
            }

            if (permissions.includes(5)) {
                $("#permissionsOption").prop('hidden', false);
            } else {
                $("#permissionsOption").prop('hidden', true);
            }       

            if (permissions.includes(6)) {
                $("#productsOption").prop('hidden', false);
            } else {
                $("#productsOption").prop('hidden', true);
            }

            if (permissions.includes(7)) {
                $("#taxesOption").prop('hidden', false);
            } else {
                $("#taxesOption").prop('hidden', true);
            }

            if (permissions.includes(8)) {
                $("#schoolsOption").prop('hidden', false);
            } else {
                $("#schoolsOption").prop('hidden', true);
            }
        } else if (permissions.includes(10)) {
            $("#billsOption").prop('hidden', false);
            $("#crudOptions").prop('hidden', true);
        } else if (!(permissions.includes(10))) {
            $("#billsOption").prop('hidden', true);
            $("#crudOptions").prop('hidden', true);
        } else {
            $("#crudOptions").prop('hidden', true);
            $("#billsOption").prop('hidden', true);
            $("#permissionsOption").prop('hidden', true);
            $("#taxesOption").prop('hidden', true);
            $("#productsOption").prop('hidden', true);
            $("#schoolsOption").prop('hidden', true);
            $("#membershipsOption").prop('hidden', true);
            $("#tenderssOption").prop('hidden', true);
        }
    }

    this.LogOut = function () {

        localStorage.clear();
        this.CheckLocalStorage();
        window.location.href = "\Index";
    }

    this.CheckPremium = function () {

        const obtenerUsuario = parseInt(localStorage.getItem("idUsuario"));
        let validacion = null;
        $("#inventory").prop('hidden', true);

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
                $("#inventory").prop('hidden', false);
            }
        });

    }
}
    

$(document).ready(function () {

    var view = new IndexView();
    view.InitView();

});