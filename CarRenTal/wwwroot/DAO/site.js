"use strict";

document.getElementById("countrylist").addEventListener('change', function () {
    
    if (document.getElementById("countrylist").value == 0) {
        console.log("1")
        document.getElementById("savebutton2").disabled = true;
    }
    else {
        document.getElementById("savebutton2").disabled = false;
    }
})

document.getElementById("pills-profile-tab").addEventListener('click', function () {
    document.getElementById("pills-home-tab").style.display = 'block';
})

document.getElementById("pills-home-tab").addEventListener('click', function () {
    document.getElementById("pills-home-tab").style.display = 'none';
    document.getElementById("countrylist").value = 0;
})

document.getElementById("pills-profile-tab2").addEventListener('click', function () {
    document.getElementById("pills-home-tab2").style.display = 'block';
})

document.getElementById("pills-home-tab2").addEventListener('click', function () {
    document.getElementById("pills-home-tab2").style.display = 'none';
    document.getElementById("countrylist2").value = null;

    
})
