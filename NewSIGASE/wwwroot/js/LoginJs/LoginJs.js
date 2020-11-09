

$(document).ready(function () {
    $("#MSG").hide();
});

login = function () {
    debugger;
    email = $("#inputEmail").val();
    senha = $("#inputSenha").val();
    $.ajax({
        type: 'POST',
        async: false,
        url: "Login/Autenticar?email=" + email + "&password=" + senha,
        success: function (data) {
            if (data.erro) {
                $("#MSG").show();
                $("#msgRetorno").html(data.strErro);
            } else {
                debugger;
                window.location.href = data.url;
            }
        }
    });
};

recuperarSenha = function () {
    debugger;
    email = $("#inputEmail").val();
    $.ajax({
        type: 'POST',
        async: true,
        url: "RecuperarSenha?email=" + email,
        success: function (data) {
            if (data.erro) {
                $("#MSG").show();
                $("#msgRetorno").html(data.strErro.mensagens[0].message);
            }
            else {
                $("#MSG").show();
                $("#msgRetorno").html(data.strErro);
            }
        }
    });
};