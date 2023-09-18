(function () {
    var clipboard = new ClipboardJS('span.test', {
        target: function () {
            return document.getElementById("copy_to_clipboard");
        }
    });
})();
