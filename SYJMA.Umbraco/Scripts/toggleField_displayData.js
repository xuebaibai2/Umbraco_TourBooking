$(document).ready(function () {
    $("button").click(function () {
        var id = $(this).children('span').eq(1).attr('id')
        var cla = $(this).children('span').eq(1).attr('class')
        updateIcon(id, cla);
        $(this).next().slideToggle(400);
    });
});

function updateIcon(id, cla) {
    $('#' + id).removeClass();
    if (cla == 'fa fa-caret-square-o-down pull-right') {
        $('#' + id).addClass('fa fa-caret-square-o-up pull-right');
    } else if (cla == 'fa fa-caret-square-o-up pull-right') {
        $('#' + id).addClass('fa fa-caret-square-o-down pull-right');
    }
}