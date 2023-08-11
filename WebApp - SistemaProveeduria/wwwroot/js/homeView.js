function HomeView() {

    this.ViewName = "HomeView";
    var self = this;

    this.InitView = function () {

        this.CheckLocalStorage();
    }

    this.CheckLocalStorage = function () {

        if (localStorage.getItem("idUsuario") !== null) {
            $("#cardsIndex").prop('hidden', true);
            $("#tendersCard").prop('hidden', false);

        } else {
            $("#cardsIndex").prop('hidden', false);
            $("#tendersCard").prop('hidden', true);
        }
    }
}


$(document).ready(function () {

    var view = new HomeView();
    view.InitView();

});