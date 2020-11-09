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
    let hr = 0;
    let mm = 0;
    let ss = 0;
    let timer = null;

    const fixTime = (v) => {
        if (v <= 9) {
            return "0" + v;
        }
        return "" + v;
    }

    const syncTime = (cb) => {
        fetch('/Activity/ChangeTime', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                idActivityAssistance: asjdlfkajsdflk.value,
                timeOcurred: time.textContent
            })
        })
            .then(r => r.json())
            .then(r => {
                if (!r.finished) {
                    window.location.href = '/Activity/Index';
                    return;
                }
                let t = parseInt(r.timeOcurred.replace(/:/g, ''));
                let t2 = parseInt(time.textContent.replace(/:/g, ''));
                if (t > t2) {
                    time.textContent = r.timeOcurred;
                    let sps = time.textContent.split(':');
                    hr = parseInt(sps[0]) | 0;
                    mm = parseInt(sps[1]) | 0;
                    ss = parseInt(sps[2]) | 0;
                }
                if (cb) {
                    cb(r);
                }
            })
            .catch(e => {
                console.log(e);
                if (cb) {
                    cb(null);
                }
            });
    };

    const calcTime = () => {
        if (!timer) {
            timer = setInterval(() => {
                ss++;
                if (ss == 60) {
                    ss = 0;
                    mm++;
                    if (mm == 60) {
                        mm = 0;
                        hr++;
                        if (hr == 3) {
                            clearInterval(timer);
                            timer = null;
                            //TODO FINISH
                        }
                    }
                }
                time.textContent = fixTime(hr) + ":" + fixTime(mm) + ":" + fixTime(ss);
                syncTime();

            }, 1000);
        } else {
            clearInterval(timer);
            timer = null;
        }
    }

    const init = () => {
        if (time.textContent == '00:00:00') {
            btnStart.parentNode.removeChild(btnPause);
            btnStart.parentNode.removeChild(btnStop);
        } else {
            btnStart.parentNode.removeChild(btnStart);
            let sps = time.textContent.split(':');
            hr = parseInt(sps[0]) | 0;
            mm = parseInt(sps[1]) | 0;
            ss = parseInt(sps[2]) | 0;
        }
        btnStart.addEventListener('click', () => {
            btnStart.parentNode.appendChild(btnStop);
            btnStart.parentNode.appendChild(btnPause);
            btnStart.parentNode.removeChild(btnStart);
            calcTime();
            btnPause.textContent = 'Pausar'
        });
        btnPause.addEventListener('click', () => {
            (btnPause.textContent == 'Pausar') ? btnPause.textContent = 'Continuar' : btnPause.textContent = 'Pausar';
            calcTime();
        });
        btnStop.addEventListener('click', () => {
            if (timer) {
                clearInterval(timer);
                timer = null;
            }
            syncTime(r => {
                if (r) {
                    fetch('/Activity/StopSessionActivity', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({
                            idActivityAssistance: asjdlfkajsdflk.value,
                            timeOcurred: time.textContent
                        })
                    })
                        .then(r => r.json())
                        .then(r => {
                            if (r.res) {
                                window.location.href = '/Activity/Index';
                            } else {
                                alert('Error al finalizar la actividad');
                            }
                        })
                        .catch(e => {
                            console.log(e);
                        });
                } else {
                    alert('Error al hacer sincronizar los cambios');
                }
            });
        });
    };

    init();

})();