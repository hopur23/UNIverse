$(document).ready(function () {
    $('body').on("click", '#moreLink', function (e) {
        e.preventDefault();

        var tempLinkText = $(this).html();
        //$(this).html("<img src='/images/ajax-loader.gif' />");
        $(this).html('loading');

        var url = $(this).prop('href');
        var data = { postToID: $('#wallPosts > div:last').data('postref') };

        $.post(url, data, function (response) {
            $('#wallPosts').append(response);
            $(this).html(tempLinkText);
        });

    });
});