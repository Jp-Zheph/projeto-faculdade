

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
        data: 'option=validate_login&str_login=' + $("#form-login").val() + '&str_password=' + $("#form-password").val(),
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
        async: false,
        url: "Login/RecuperarSenha",
        data: { email: email },
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