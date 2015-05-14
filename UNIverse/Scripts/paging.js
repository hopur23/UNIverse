$(document).ready(function () {
    $('body').on("click", '#moreLink', function (e) {
        e.preventDefault();

        var eLink = $(this);

        var tempLinkText = eLink.html();
        //$(this).html("<img src='/images/ajax-loader.gif' />");
        eLink.html('loading');

        var url = $(this).prop('href');
        var data = { postToID: $('#wallPosts > div:last').data('postref') };
        if ($('#groupId').length) {
            data.groupId = $('#groupId').val();
        }
        if ($('#Tag').length) {
            data.Tag = $('#Tag').val();
        }

        $.post(url, data, function (response) {

            if (response == '') {
                eLink.replaceWith("<div>All posts loaded</div>")
            }
            else {
                $('#wallPosts').append(response);
                eLink.html(tempLinkText);
            }

        });

    });
});