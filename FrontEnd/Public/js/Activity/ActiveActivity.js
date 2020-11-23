(() => {

    const getE = a => {
        if (a.startsWith('.') || a.startsWith('#')) {
            return document.querySelector(a);
        } else {
            return document.getElementsByName(a);
        }
    };

    const ndom = a => {
        return document.createElement(a);
    };

    const showDiagError = (msg) => {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: msg
        })
    }


    const btnStart = getE('#btn-start');
    const btnPause = getE('#btn-pause');
    const btnStop = getE('#btn-stop');
    const asjdlfkajsdflk = getE('#asjdlfkajsdflk');
    const time = getE('#time');

    const asignateTimeNum = (str) => {
        time.textContent = str;
    };

    (function ($) {
        "use strict";
        var ws = $.connection.activityWs;

        ws.client.updateTime = (r) => {
            if (r.timeOcurred) {
                asignateTimeNum(r.timeOcurred);
                if (btnStart.parentNode) {
                    btnStart.parentNode.appendChild(btnStop);
                    btnStart.parentNode.appendChild(btnPause);
                    btnStart.parentNode.removeChild(btnStart);
                }
                if (!r.res) {
                    btnPause.textContent = 'Continuar';
                }
                if (btnPause.textContent != 'Pausar' && r.res) {
                    btnPause.textContent = 'Pausar';
                }
            } else {
                btnPause.textContent = 'Continuar';
            }
        };

        ws.client.stopCurTime = (e) => {
            asignateTimeNum(e);
        };

        ws.client.stopCurActivitie = (e) => {
            if (e) {
                window.location.href = '/Activity/Index';
            }
        };

        $.connection.hub.start().done(() => {
            ws.server.startActivity(window.location.href);
        });

        if (time.textContent == '00:00:00') {
            btnStart.parentNode.removeChild(btnPause);
            btnStart.parentNode.removeChild(btnStop);
        } else {
            btnStart.parentNode.removeChild(btnStart);
            asignateTimeNum(time.textContent);
        }
        btnStart.addEventListener('click', () => {
            btnStart.parentNode.appendChild(btnStop);
            btnStart.parentNode.appendChild(btnPause);
            btnStart.parentNode.removeChild(btnStart);
            ws.server.changeTime(asjdlfkajsdflk.value);
            btnPause.textContent = 'Pausar';
        });
        btnPause.addEventListener('click', () => {
            ws.server.changeTime(asjdlfkajsdflk.value);
        });
        btnStop.addEventListener('click', () => {
            ws.server.stopSessionActivity(asjdlfkajsdflk.value);
        });


    })(jQuery);

})();