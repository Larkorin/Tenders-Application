//Definicion de la clase ReportsVIew
function ReportsView() {

    this.ViewName = "ReportsView";
    this.ApiService = "";

    this.InitView = () => {
        console.log('Hola')
        this.LoadTable();
    }

    this.LoadTable = () => {

        let urlService = "";

        $('#tblReports').dataTable();
    }
}

//Instaciamiento inicial de la clase
//Se ejecuta al terminar la carga de la vista
$(document).ready(() => {
    const view = new ReportsView();
    view.InitView();
})