function ControlActions() {

	this.URL_API = "https://localhost:7123/api/";

	this.GetUrlApiService = function (service) {
		return this.URL_API + service;
	}

	this.GetTableColumsDataName = function (tableId) {
		var val = $('#' + tableId).attr("ColumnsDataName");

		return val;
	}

	this.FillTable = function (service, tableId, refresh) {

		if (!refresh) {
			columns = this.GetTableColumsDataName(tableId).split(',');
			var arrayColumnsData = [];


			$.each(columns, function (index, value) {
				var obj = {};
				obj.data = value;
				arrayColumnsData.push(obj);
			});
			//Esto es la inicializacion de la tabla de data tables segun la documentacion de 
			// datatables.net, carga la data usando un request async al API
			$('#' + tableId).DataTable({
				"processing": true,
				"ajax": {
					"url": this.GetUrlApiService(service),
					dataSrc: 'Data'
				},
				"columns": arrayColumnsData
			});
		} else {
			//RECARGA LA TABLA
			$('#' + tableId).DataTable().ajax.reload();
		}

	}

	this.GetSelectedRow = function () {
		var data = sessionStorage.getItem(tableId + '_selected');

		return data;
	};

	this.BindFields = function (formId, data) {
		console.log(data);
		$('#' + formId + ' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			this.value = data[columnDataName];
		});
	}

	this.GetDataForm = function (formId) {
		var data = {};

		$('#' + formId + ' *').filter(':input').each(function (input) {
			var columnDataName = $(this).attr("ColumnDataName");
			data[columnDataName] = this.value;
		});

		console.log(data);
		return data;
	}

	this.ShowMessage = function (type, message) {
		if (type == 'E') {
			$("#alert_container").removeClass("alert alert-success alert-dismissable")
			$("#alert_container").addClass("alert alert-danger alert-dismissable");
			$("#alert_message").text(message);
		} else if (type == 'I') {
			$("#alert_container").removeClass("alert alert-danger alert-dismissable")
			$("#alert_container").addClass("alert alert-success alert-dismissable");
			$("#alert_message").text(message);
		}
		$('.alert').show();
	};

	/* ACCIONES VIA AJAX, O ACCIONES ASINCRONAS*/
	this.PostToAPIv1 = function (service, data, callBackFunction) {
		var jqxhr = $.post(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			console.log(response.Message);
			//ctrlActions.ShowMessage('I', response.Message);

			if (callBackFunction) {
				callBackFunction(response.Data);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				//ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};


	this.PostToAPI = function (service, data, callBackFunction) {

		$.ajax({
			type: "POST",
			url: this.GetUrlApiService(service),
			data: JSON.stringify(data),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {
				if (callBackFunction) {
					callBackFunction(response);
				}
			},
			error: function (jqXHR, textStatus, errorThrown) {
				console.log("Hubo un error al enviar la solicitud: " + textStatus + ", " + errorThrown);
			}
		});
	};


	this.PutToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.put(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callBackFunction(response);
			}

		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.DeleteToAPI = function (service, data, callBackFunction) {
		var jqxhr = $.delete(this.GetUrlApiService(service), data, function (response) {
			var ctrlActions = new ControlActions();
			ctrlActions.ShowMessage('I', response.Message);
			if (callBackFunction) {
				callBackFunction(response);
			}
		})
			.fail(function (response) {
				var data = response.responseJSON;
				var ctrlActions = new ControlActions();
				ctrlActions.ShowMessage('E', data.ExceptionMessage);
				console.log(data);
			})
	};

	this.GetToApi = function (service, onSuccess, onError, onHttpResponse) {
		var url = this.GetUrlApiService(service);
		var xhr = new XMLHttpRequest();
		xhr.open("GET", url, true);
		xhr.onreadystatechange = function () {
			if (this.readyState === XMLHttpRequest.DONE) {
				if (this.status === 200) {
					if (onSuccess) {
						onSuccess(JSON.parse(this.responseText));
					}
				} else {
					if (onHttpResponse) {
						onHttpResponse(this);
					}
					if (onError) {
						onError();
					}
				}
			}
		};
		xhr.send();
	};


	this.GetToApiV2 = function (service, callBackFunction) {
		$.ajax({
			type: "GET",
			url: this.GetUrlApiService(service),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (data) {
				if (callBackFunction) {
					callBackFunction(data);
				}
			},
			error: function (jqXHR, textStatus, errorThrown) {
				if (callBackFunction) {
					console.log(jqXHR.responseJSON);
					callBackFunction(jqXHR.responseJSON)
				}
			}
		});
	};
}

//Custom jquery actions
$.put = function (url, data, callback) {
	if ($.isFunction(data)) {
		type = type || callback,
			callback = data,
			data = {}
	}
	return $.ajax({
		url: url,
		type: 'PUT',
		success: callback,
		data: JSON.stringify(data),
		contentType: 'application/json'
	});
}

$.delete = function (url, data, callback) {
	if ($.isFunction(data)) {
		type = type || callback,
			callback = data,
			data = {}
	}
	return $.ajax({
		url: url,
		type: 'DELETE',
		success: callback,
		data: JSON.stringify(data),
		contentType: 'application/json'
	});
}

$.post = function (url, data, callback) {
	if ($.isFunction(data)) {
		type = type || callback,
			callback = data,
			data = {}
	}
	return $.ajax({
		url: url,
		type: 'POST',
		success: callback,
		data: JSON.stringify(data),
		contentType: 'application/json'
	});
}
