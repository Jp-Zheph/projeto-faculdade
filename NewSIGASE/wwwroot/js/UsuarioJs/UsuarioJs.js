
$(document).ready(function () {
    $("#cep").blur(function () {
        var cep = $("#cep").val();

        if (cep == "") {
            $("#cep").addClass("is-invalid");
            $("#cep").focus();
        }
        else {
            $("#cep").removeClass("is-invalid");
        }

        $("#cidade").val("");
        $("#bairro").val("");
        $("#logradouro").val("");
        $("#uf").val("");

        $.ajax({
            type: "get",
            url: "/Usuarios/ConsultarCep",
            data: { cep: cep },
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.erro) {
                    var tipo = data.strErro.tipo;

                    $('#alert_container #alert_div').remove();

                    $.each(data.strErro.mensagens, function (i, d) {
                        $('#alert_container').append('<div id="alert_div" class="alert alert-' + tipo + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + d.property + '!</strong> <span>' + d.message + '</span></div>');
                    });

                }
                else {
                    $('#alert_container #alert_div').remove();

                    $("#cidade").val(data.endereco.cidade);
                    $("#bairro").val(data.endereco.bairro);
                    $("#logradouro").val(data.endereco.logradouro);
                    $("#uf").val(data.endereco.uf);
                }
            }
        });
    })

    if ($("#IdUsu").val() == $("#Id").val()) {
        $("#selectAtivo").attr('readonly', true);
    }
});