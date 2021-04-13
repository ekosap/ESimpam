// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function Loading(isLoad, button, text) {
    if (isLoad) {
        button.html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
    }
    else {
        button.html(text);
    }
    button.attr('disabled', isLoad);
}
function Combobox(selectElement) {
    selectElement.select2({
        theme: 'bootstrap4',
        width: '100%'
    });
}