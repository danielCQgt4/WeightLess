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

    window.addEventListener('load', () => {


        const selectActivitiesBase = getE('#select-activities-base');
        const selectType = getE('#select-type');
        const contTitleActivities = getE('#cont-title-activities');
        const btnAddActivitie = getE('#admDivCheck');
        const contFormActivitie = getE('#cont-form-activities');
        const activities = [];

        const newFormActivitie = () => {
            const divForm = ndom('div');
            const hidden = ndom('input');
            hidden.type = 'hidden';
            hidden.name = 'activities';
            divForm.appendChild(hidden);

            const divActivitie = ndom('div');
            divActivitie.className = 'form-row';
            const divActivitieInside = ndom('div');
            divActivitieInside.className = 'form-group col-md-10 col-lg-8';
            divActivitie.appendChild(divActivitieInside);
            const labelActivitie = ndom('label');
            labelActivitie.textContent = 'Actividad';
            divActivitieInside.appendChild(labelActivitie);
            const selectActivities = ndom('select');
            selectActivities.addEventListener('change', () => {
                hidden.value = selectActivities.value + ':' + txtDescription.value;
            });
            selectActivities.className = 'form-control';
            for (let act of activities) {
                const op = ndom('option');
                op.value = act.value;
                op.textContent = act.desc;
                selectActivities.appendChild(op);
            }
            divActivitieInside.appendChild(selectActivities);
            divForm.appendChild(divActivitie);

            const divDescription = ndom('div');
            divDescription.className = 'form-row';
            const divDescriptionInside = ndom('div');
            divDescriptionInside.className = 'form-group col-md-10 col-lg-8';
            divDescription.appendChild(divDescriptionInside);
            const labelDescription = ndom('label');
            labelDescription.textContent = 'Descripción de la actividad';
            divDescriptionInside.appendChild(labelDescription);
            const txtDescription = ndom('textarea');
            txtDescription.addEventListener('keyup', () => {
                hidden.value = selectActivities.value + ':' + txtDescription.value;
            });
            txtDescription.className = 'form-control';
            txtDescription.placeholder = 'Ingresa una descripcion para la actividad';
            txtDescription.cols = 50;
            txtDescription.rows = 4;
            divDescriptionInside.appendChild(txtDescription);
            divForm.appendChild(divDescription);

            const divBtn = ndom('div');
            divBtn.className = 'd-flex';
            const btnDel = ndom('button');
            btnDel.className = 'btn btn-danger';
            btnDel.textContent = 'Eliminar';
            divBtn.appendChild(btnDel);
            divForm.appendChild(divBtn);
            const hr = ndom('hr');
            divForm.appendChild(hr);
            return { divForm, btnDel };
        }

        const calcAvitivities = () => {
            if (selectActivitiesBase) {
                const ops = selectActivitiesBase.childNodes;
                for (let o of ops) {
                    if (o.textContent != '\n') {
                        activities.push({
                            value: o.value,
                            desc: o.textContent
                        });
                    }
                }
                console.log(activities);
            }
        };

        let els = [];
        if (btnAddActivitie) {
            btnAddActivitie.addEventListener('click', (e) => {
                e.preventDefault();
                console.log(activities.length, els.length);
                if ((activities.length - 1) <= els.length) {
                    showDiagError('Ya no hay mas actividades');//TODO swift alert
                    return;
                }
                const act = newFormActivitie();
                els.push(act);
                contFormActivitie.appendChild(act.divForm);
                act.btnDel.addEventListener('click', () => {
                    contFormActivitie.removeChild(act.divForm);
                    els = els.filter(o => o != act);
                });
            });
        }

        if (selectType) {
            const calculate = () => {
                btnAddActivitie.style.display = 'none';
                contTitleActivities.style.display = 'none';
                if (selectType.value == 'A') {
                    btnAddActivitie.style.display = '';
                    contTitleActivities.style.display = '';
                } else {
                    for (let t of els) {
                        contFormActivitie.removeChild(t.divForm);
                    }
                    els = [];
                }
            };
            selectType.addEventListener('change', calculate);
            calculate();
        }

        calcAvitivities();
    });

})();