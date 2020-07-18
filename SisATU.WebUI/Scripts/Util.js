
function ValidarCelular(elemento, esTelefono) {
    if ($(elemento).val().length == 0) {
        if (esTelefono) {
            MensajeAlerta("error", "Ingresar su número de celular o teléfono.")
        } else {
            MensajeAlerta("error", "Ingresar su número de celular.")
        }

        $(elemento).focus();
        $(elemento).parent().addClass('has-error');
        return false;
    } else {
        if (esTelefono) {
            if ($(elemento).val().length < 7 || $(elemento).val().length > 9) { //telefono min 7 y max 9
                MensajeAlerta("error", "Ingresar un número válido.")
                $(elemento).focus();
                $(elemento).parent().addClass('has-error');
                return false;
            } else {
                if ($(elemento).parent().hasClass('has-error')) {
                    $(elemento).parent().removeClass('has-error');
                }
            }
        } else {
            if ($(elemento).val().length > 0 && $(elemento).val().length < 9) {
                MensajeAlerta("error", "Ingresar un número válido.");
                $(elemento).focus();
                $(elemento).parent().addClass('has-error');
                return false;
            } else {
                if ($(elemento).parent().hasClass('has-error')) {
                    $(elemento).parent().removeClass('has-error');
                }
            }
        }

    }
    return true;
}

function ValidarTelefono(elemento) {
    if ($(elemento).val().length == 0) {
        MensajeAlerta("error", "Ingresar su número de teléfono");
        $(elemento).focus();
        $(elemento).parent().addClass('has-error');
        return false;
    } else {
        if ($(elemento).val().length > 0 && $(elemento).val().length < 7) { //telefono min 7 y max 9
            MensajeAlerta("error", "Ingresar un número de teléfono válido.");
            $(elemento).focus();
            $(elemento).parent().addClass('has-error');
            return false;
        } else {
            if ($(elemento).parent().hasClass('has-error')) {
                $(elemento).parent().removeClass('has-error');
            }
        }
    }
    return true;
}

function validarEmail(elemento) {
    if ($(elemento).val().length == 0) {
        MensajeAlerta("error", "Es necesario ingresar el correo electrónico.")
        $(elemento).parent().addClass('has-error');
        $(elemento).focus();
        return false;
    } else {

        if (!SoloCorreo($(elemento).val())) {
            MensajeAlerta("error",  "Debe ingresar un correo válido.")
            $(elemento).parent().addClass('has-error');
            $(elemento).focus();
            return false;
        } else {
            if ($(elemento).parent().hasClass('has-error')) {
                $(elemento).parent().removeClass('has-error');
            }
        }
    }
    return true;
}

function ValidarDireccion(elemento) {
    if ($(elemento).val().length == 0) {
        MensajeAlerta("error", "Es necesario ingresar la dirección actual.")
        $(elemento).focus();
        $(elemento).parent().addClass('has-error');
        return false;
    } else {
        if ($(elemento).val().length < 10) { //validando como min tiene que tener 10 caracteres
            MensajeAlerta("error", "Verificar dirección ingresada.")
            $(elemento).focus();
            $(elemento).parent().addClass('has-error');
            return false;
        } else {
            if ($(elemento).parent().hasClass('has-error')) {
                $(elemento).parent().removeClass('has-error');
            }
        }
    }
    return true;
}

function ValidarNumeroDocumento(elemento) {
    if ($(elemento).val().length == 0) {
        MensajeAlerta("error", "Debe ingresar el nro de documento.")
        $(elemento).focus();
        $(elemento).parent().addClass('has-error');
        return false;
    } else {
        if ($(elemento).parent().hasClass('has-error')) {
            $(elemento).parent().removeClass('has-error');
        }
    }
    return true;
}

function ValidarTipoDocumento(elemento) {
    if ($(elemento).val() == 0) {
        MensajeAlerta("error", "Debe elegir un tipo de documento.");
        if (!$(elemento).is('[readonly]')) {
            $(elemento).focus();
        }
        $(elemento).parent().addClass('has-error');
        return false;
    } else {
        if ($(elemento).parent().hasClass('has-error')) {
            $(elemento).parent().removeClass('has-error');
        }
    }
    return true;
}




function MensajeAlerta(tipo, mensaje) {
    var opciones = {
        "closeButton": true,
        "debug": false,
        "progressBar": true,
        "positionClass": "toast-top-center",
        "showDuration": "400",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    if (tipo === "success") {
        toastr.success(mensaje, 'Información', opciones);
    } else if (tipo === "error") {
        toastr.error(mensaje, 'Información', opciones);
    } else if (tipo === "info") {
        toastr.info(mensaje, 'Información', opciones);
    } else if (tipo === "warning") {
        toastr.warning(mensaje, 'Información', opciones);
    }
}

function parseFecha(fecha) {
    if (fecha == null) {
        var newFecha = new Date();
    } else {
        var from = fecha.split("/");
        var dia = from[0];
        var mes = from[1];
        var anio = from[2];
        var newFecha = anio + "-" + mes + "-" + dia;
    }
    return new Date(newFecha);
}


function Cargando($element) {
    $element.empty();
    var htmlCargando = "<div id='divCargando' style='height: 100%; padding-top: 14%;'>" +
        "<div class='sk-spinner sk-spinner-fading-circle'>" +
        "<div class='sk-circle1 sk-circle'></div>" +
        "<div class='sk-circle2 sk-circle'></div>" +
        "<div class='sk-circle3 sk-circle'></div>" +
        "<div class='sk-circle4 sk-circle'></div>" +
        "<div class='sk-circle5 sk-circle'></div>" +
        "<div class='sk-circle6 sk-circle'></div>" +
        "<div class='sk-circle7 sk-circle'></div>" +
        "<div class='sk-circle8 sk-circle'></div>" +
        "<div class='sk-circle9 sk-circle'></div>" +
        "<div class='sk-circle10 sk-circle'></div>" +
        "<div class='sk-circle11 sk-circle'></div>" +
        "<div class='sk-circle12 sk-circle'></div>" +
        "</div>" +
        "</div>"
    if ($("#divCargando").length == 0) {
        $element.after(htmlCargando);
    } else {
        $("#divCargando").show();
    }
}

function CargandoTarjeta($elemento, divClase, height) {
    var top = parseFloat((height - 22) * 0.50);
    $elemento.empty();
    var htmlCargando = "<div class='" + divClase + "' id='DivCargando'>" +
                       "<div class='ibox'>" +
                       "<div class='ibox-title'>" +
                       "<h5 class='colorLetraATU'>Cargando...</h5>" +
                       "</div>" +
                       "<div class='ibox-content' style='height:" + height + "px; padding-top:" + top + "px'>" +
                       "<div>" +
                            "<div class='sk-spinner sk-spinner-fading-circle'>" +
                             "<div class='sk-circle1 sk-circle'></div>" +
                                "<div class='sk-circle2 sk-circle'></div>" +
                                "<div class='sk-circle3 sk-circle'></div>" +
                                "<div class='sk-circle4 sk-circle'></div>" +
                                "<div class='sk-circle5 sk-circle'></div>" +
                                "<div class='sk-circle6 sk-circle'></div>" +
                                "<div class='sk-circle7 sk-circle'></div>" +
                                "<div class='sk-circle8 sk-circle'></div>" +
                                "<div class='sk-circle9 sk-circle'></div>" +
                                "<div class='sk-circle10 sk-circle'></div>" +
                                "<div class='sk-circle11 sk-circle'></div>" +
                                "<div class='sk-circle12 sk-circle'></div>" +
                            "</div>" +
                       "</div>" +
                       "</div>" +
                       "</div>" +
                       "</div>"
    $elemento.append(htmlCargando);
}

function ValidaRegistroDuplicado(ValorAbuscar, tbTabla, repite) {
    var EncuentraRegistro = 0;
    var CodArticuloAux = 0;
    // Recorrer la tabla
    if (repite == null || repite == 0) {
        $(tbTabla).children('tr').each(function () {
            CodArticuloAux = this.cells[1].children[0].value;
            // Valida si ya esta el articulo en el detalle
            if (CodArticuloAux == ValorAbuscar) {
                EncuentraRegistro = 1;
            }
        });
    }
    return EncuentraRegistro;
}

var MenuLongitud = [[10, 25, 50, -1], [10, 25, 50, "Todos"]];
var PageInfo = {
    page: 0,
    pages: 0,
    start: 0,
    end: 0,
    length: 10,
    recordsTotal: 0,
    recordsDisplay: 0,
    serverSide: true
};

var mensajesDTable = {
    search: "Buscar&nbsp;:",
    lengthMenu: "Muestra _MENU_ elementos",
    info: "Mostrando _START_ a _END_ de _TOTAL_ elementos",
    infoEmpty: "Mostrando 0 a 0 de 0 entradas",
    paginate: {
        first: "Primero",
        previous: "Anterior",
        next: "Siguiente",
        last: "Último"
    },
    emptyTable: "No hay datos disponibles en la tabla",
    zeroRecords: "No se encontraron registros coincidentes",
    processing: "Cargando...",
    //search: "Rechercher&nbsp;:",
    //lengthMenu: "Afficher _MENU_ &eacute;l&eacute;ments",
    //info: "Affichage de l'&eacute;lement _START_ &agrave; _END_ sur _TOTAL_ &eacute;l&eacute;ments",
    //infoEmpty: "Affichage de l'&eacute;lement 0 &agrave; 0 sur 0 &eacute;l&eacute;ments",
    //infoFiltered: "(filtr&eacute; de _MAX_ &eacute;l&eacute;ments au total)",
    //infoPostFix: "",
    //loadingRecords: "Chargement en cours...",
    //zeroRecords: "Aucun &eacute;l&eacute;ment &agrave; afficher",
    //emptyTable: "Aucune donnée disponible dans le tableau",
    //paginate: {
    //    first: "Premier",
    //    previous: "Pr&eacute;c&eacute;dent",
    //    next: "Suivant",
    //    last: "Dernier"
    //},
    //aria: {
    //    sortAscending: ": activer pour trier la colonne par ordre croissant",
    //    sortDescending: ": activer pour trier la colonne par ordre décroissant"
    //}

}

function soloNumeros(e) {
    var keynum = window.event ? window.event.keyCode : e.which;
    if ((keynum == 8) || (keynum == 46))
        return true;
    return /\d/.test(String.fromCharCode(keynum));
}

function SoloCorreo(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function modal(id, url, bind, escape, claseModal, claseModalDialogo, ancho, alto, funcion, evento) {
    var $modal = "";

    $modal = '<div class="modal fade" id="' + id + '" tabindex="-1" role="dialog">';
    $modal += '<div class="modal-vertical">';
    $modal += '<div class="modal-dialog">';
    $modal += '<div class="modal-content">';
    $modal += '</div>';
    $modal += '</div>';
    $modal += '</div>';
    $modal += '</div>';

    $('body').append($modal);

    var $contenedor = $('body').find("#" + id);

    if (escape == 1)
        escape = true;

    if (ancho > 0)
        var divAncho = ancho;

    if (alto > 0)
        var divAlto = alto;

    if (claseModal.length > 0 && claseModal.trim != "")
        $contenedor.addClass(claseModal);

    if (claseModalDialogo.length > 0 && claseModalDialogo.trim != "") {
        $contenedor.find('.modal-dialog').addClass(claseModalDialogo);
    }

    $contenedor.find('.modal-dialog').css("width", divAncho);
    $contenedor.find('.modal-dialog').css("height", divAlto);
    //http://www.minimit.com/demos/vertical-center-bootstrap-3-modals


    //https://stackoverflow.com/a/6629045
    if (funcion != null && funcion() == true) {
        var miNuevaVentana = window.open('');
    }

    var urlSplit = url.split("#");
    var urlListar = "";
    var urlEliminar = "";
    if (urlSplit.length > 1) {
        urlListar = urlSplit[1];
        urlEliminar = urlSplit[2];
    }

    $.ajax({
        type: 'GET',
        url: url,
        async: false,
        beforeSend: function () {
        },
        success: function (data) {
            if (data.validar) {
                $('body').find("#" + id).remove();
            } else if (data.esLink) {
                miNuevaVentana.location = data.url;
                $('body').find("#" + id).remove();
            } else {
                $contenedor.find('.modal-content').html(data);
                $contenedor.find('.modal-body').css("min-height", divAlto);

                if (claseModalDialogo.indexOf("modal-fijo") >= 0) {
                    var alto = $('.dcPrincipal').height() - 150;
                    $contenedor.find('.modal-body').css('height', alto);
                }
                if (bind == 1) {
                    bindAjax($contenedor);
                } else if (bind == 2) {
                    bindAjaxForm($contenedor);
                } else if (bind == 3) {
                    bindDropzone($contenedor, urlListar, urlEliminar);
                }
                $contenedor.modal({
                    show: true,
                    keyboard: escape,
                    backdrop: 'static'
                });
            }
        },
        complete: function () {
            //desbloquearCargando($('html'))

            if (evento != null || typeof (evento) != "undefined") {
                evento.find('span').removeClass("fa fa-spinner fa-spin").addClass("fa fa-plus");
                evento.removeAttr("disabled");
            }

            if (funcion != null) {
                funcion();
            }
        },
        error: function (xhr, props) {
            if (xhr.status == 401 || xhr.status == 302) {
            } else {
                $contenedor.modal('hide');
                //mensajeDevCore(4, "Ocurrio un error inesperado por favor intenta de nuevo. <br />" + xhr.statusText);
            }
        }
    })

    //$contenedor.draggable({
    //    handle: ".modal-header",
    //    //cursor: "move",
    //});

    //contenedor.data({
    //    'originalLeft': contenedor.css('left'),
    //    'origionalTop': contenedor.css('top')
    //});

    $contenedor.on('hidden.bs.modal', function () {
        $contenedor.remove();
    })

    $.fn.modal.Constructor.prototype.enforceFocus = function () { };
}

function bindAjax(dialog) { //
    var modalId = $(dialog).attr("id");
    var l = $('.ladda-button').ladda();
    //return;
    $('form', dialog).submit(function () {
        console.log(this.action)
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            beforeSend: function () { // algun imagen de carga  //esto es antes q se abra el modal
                if (l.length > 0) {
                    l.ladda('start');
                }
                //gif de cargando o alguna para sabnes si esta cargando en el caso q demora un1 segundo mas
            },
            success: function (data) {
                if (data.success == true) {
                    $("#" + modalId).modal('hide');
                    toastr.success(data.mensaje, 'Información');

                } else {
                    toastr.warning(data.mensaje, 'Información');
                }
            },
            complete: function (data) {
                l.ladda('stop');
            },
            error: function (data) {

            }
        });
        return false;
    })
}

//para guardar imagenes
function bindAjaxForm(dialog) {
    var l = $('.ladda-button').ladda();
    var modalId = $(dialog).attr("id");
    $('form', dialog).ajaxForm({
        beforeSend: function () {
            if (l.length > 0) {
                l.ladda('start');
            }
        },
        success: function (data) {
            if (data.success == true) {
                $("#" + modalId).modal('hide');
                toastr.success(data.mensaje, 'Información');

            } else {
                toastr.warning(data.mensaje, 'Información');
            }
        },
        complete: function (data) {
            l.ladda('stop');
        },
        error: function () {

        }
    });

}
