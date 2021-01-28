// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function enableEdit() {
    document.getElementById('input1').disabled = false;
    document.getElementById('input2').disabled = false;
    document.getElementById('button').hidden = true;
}

function confirmDelete(isTrue) {
    if (isTrue) {
        $('#deleteSpan').hide();
        $('#confirmDeleteSpan').show();
        $('#deleteSelect').prop('disabled', true);
    } else {
        $('#deleteSpan').show();
        $('#confirmDeleteSpan').hide();
        $('#deleteSelect').prop('disabled', false);
    }
}

function sendSelect() {
    $('#deleteSelect').prop('disabled', false);
}

$(function () {
    $('.text-reset').on('click', function () {
        $('.fa', this)
            .toggleClass('fa-caret-right')
            .toggleClass('fa-caret-down');
    });
});

function expandCollapse(isTrue) {
    if (isTrue) {
        $('.collapse').collapse('show');
        $('#btnShow').hide();
        $('#btnHide').show();
    } else {
        $('.collapse').collapse('hide');
        $('#btnHide').hide();
        $('#btnShow').show();
    }
}