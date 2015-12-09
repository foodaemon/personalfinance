$(function () {
    $('#input_apiKey').off();
    $('#input_apiKey').change(function() {
        var key = $('#input_apiKey')[0].value;
        if (key && key.trim() != "") {
            swaggerUi.api.clientAuthorizations.add("key", new SwaggerClient.ApiKeyAuthorization("Authorization", "Token " + key, "header"));
        }
    });
})();