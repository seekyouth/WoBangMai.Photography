function mask(id) {
    loading = new ol.loading({ id: id });
    loading.show();
};
(function ($) {
    var l;
    $.fn.mask = function () {
        var id=$(this).attr("id");
        //loading = new ol.loading({ id: id });
        //loading.show();
        l = new ol.loading({ id: id });
        l.show();
    };
    $.fn.unmask = function () {
        //var current = $(this);
        //var content = $(this).parent().parent();
        //$(this).parent().remove();
        //content.append(current);
        if (l) {
            l.hide();
        }
    };

})(jQuery);