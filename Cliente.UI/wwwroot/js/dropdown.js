$(document).ready(function () {
    var $cidade = $("#CidadeId");

    $('#EstadoId').change(function () {
        var optionSelected = $(this).find("option:selected");
        var estadoid = optionSelected.val();

        data = { 'id': estadoid };
        $.get('GetCidades/' + estadoid, data, function (result) {
            console.log(result);
            $cidade.empty();
            $cidade.append('<option>' + 'Selecione' + '</option>');
            for (var i = result.length - 1; i >= 0; i--) {
                $cidade.append($("<option></option>")
                    .attr("value", result[i].id)
                    .text(result[i].nome));
            };
        });
    });
});
