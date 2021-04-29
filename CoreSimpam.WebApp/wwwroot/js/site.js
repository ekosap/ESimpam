// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function buildMenu(item, parent) {
    $.each(item, function () {
        let li = $('<li class="nav-item"></li>');
        let attributArrow = $('<a href="' + this.controllerName + '" class="nav-link"><i class="nav-icon ' + (this.iconCss || "fas fa-th") + '"></i><p>' + this.screenName + '<i class="right fas fa-angle-left"></i></p></a>');
        /**/
        let attributLink = $('<a href="/' + this.controllerName + '/' + (this.actionName || '') + '" class="nav-link"><i class="' + (this.iconCss || "far fa-circle") + ' nav-icon"></i><p>' + this.screenName + '</p></a>');
        /**/
        li.appendTo(parent);
        if (this.screens && this.screens.length > 0) {
            attributArrow.appendTo(li);
            let ul = $('<ul class="nav nav-treeview"></ul>');
            ul.appendTo(li);
            buildMenu(this.screens, ul);
        }
        else {
            attributLink.appendTo(li);
        }
    });
}

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