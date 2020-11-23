(() => {

    (function ($) {

        "use strict";
        var ws = $.connection.activityWs;

        ws.client.newActivity = (e) => {
            console.log(e);
            window.location.href = e;
        };

        $.connection.hub.start().done((e) => {
            console.log('Connected', e);
        });

    })(jQuery);

})();