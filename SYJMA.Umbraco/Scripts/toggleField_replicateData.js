$(document).ready(function () {
    var c = false;
    $('#checkbox').checked = false;
    $('#checkbox').change(function () {
        c = this.checked ? true : false;
        $(".invoice").toggle();
        replicateField(c, '#title', '#title_i');
        replicateField(c, '#firstname', '#firstname_i');
        replicateField(c, '#surename', '#surename_i');
        replicateField(c, '#email', '#email_i');
        setHiddenInput(c);
    });
});
function replicateField(flag, emi_id, rec_id) {

    if (flag === true) {
        $(rec_id).val($(emi_id).val());
        $(emi_id).on('input', function () {
            $(rec_id).val($(emi_id).val());
        })
    }
    if (flag === false) {
        $(rec_id).val('');
        $(emi_id).off('input')
    }
}
function setHiddenInput(flag) {
    if (flag === true) {
        //$('input[name="IsSameContact"]').val('true');
        $('#IsSameContact').val('true');
    }
    if (flag === false) {
        //$('input[name="IsSameContact"]').val('false');
        $('#IsSameContact').val('false');
    }
}