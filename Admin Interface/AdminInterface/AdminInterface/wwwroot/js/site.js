// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let dropdownClicks = 0;

function setupPage() {
    setupActivePage();
    setupNavbar();
    setupDashboard();
}

function setupActivePage() {
    let pages = document.getElementsByClassName('navbarPageSpan');
    // add active class to specified page
    pages[activePageIndex].classList.add('active');
}

function setupNavbar() {
    let pages = document.getElementsByClassName('navbarPageSpan');
    // for each page
    for (let i = 0; i < pages.length; i++) {
        // add to dropdown menu
        let newListItem = document.createElement('li');
        newListItem.classList.add('navDropdownPage');
        if (pages[i].classList.contains('active')) {
            newListItem.classList.add('active');
        }
        newListItem.innerHTML = pages[i].innerHTML;
        document.getElementsByClassName('navDropdownPagesList')[0].appendChild(newListItem);
    }
}

function navDropdownMenuClick() {
    let lines = document.getElementsByClassName('navDropdownMenuBtnLine');
    dropdownClicks++;

    // if menu not showing
    if (dropdownClicks % 2 === 1) {
        // open
        document.getElementsByClassName('navDropdownPagesList')[0].className = 'navDropdownPagesList open';
        lines[0].className = 'navDropdownMenuBtnLine open1';
        lines[1].className = 'navDropdownMenuBtnLine open2';

        // if menu is showing
    } else {
        // close
        document.getElementsByClassName('navDropdownPagesList')[0].className = 'navDropdownPagesList close';
        lines[0].className = 'navDropdownMenuBtnLine';
        lines[1].className = 'navDropdownMenuBtnLine close';
    }
}

function setupDashboard() {
    //setupOrdersWidget();
}

function setupOrdersWidget() {
    let selectedTargets = document.getElementsByClassName('selectedTarget');
    if (selectedTargets.length > 0) {
        let target = selectedTargets[0];

        try {
            let targetValue = parseInt(target.getAttribute('data-value'));

            let progressValue = document.getElementsByClassName('progressValue')[0].innerText;
            let integerProgressValue = parseInt(progressValue);

            // in decimal form (i.e. 0.5)
            var percentComplete = (progressValue / targetValue);
            // cap at 100%
            if (percentComplete > 1) {
                percentComplete = 1;
            }

            let circlePerimeter = (30 * 2 * 3.14);
            let progressLength = (percentComplete * circlePerimeter);
            console.log(`progressLength = ${progressLength}`);

            animateChart(progressLength);

        } catch (err) {
            //console.log("error setting up orders widget: error getting data value from element");
        }

    } else {
        //console.log("error setting up orders widget: error getting selected target");
    }
}

function animateChart(progress) {
    // reset animation property
    $('.progressBarThumb').css('animation', '');
    let root = document.documentElement;
    root.style.setProperty('--chartProgress', progress);
    $('.progressBarThumb').css('animation', 'progress 1.2s linear forwards');
}