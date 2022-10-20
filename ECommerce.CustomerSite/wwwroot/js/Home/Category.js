const $ = document.querySelector.bind(document)
const $$ = document.querySelectorAll.bind(document)

// Show Menu list
const menu = $('#menu')
var count_click_menu = true
menu.onclick = function (e) {
    let listMenu = $('#listMenu')
    if (count_click_menu) {
        listMenu.style.display = 'block'
        count_click_menu = false
    }
    else {
        listMenu.style.display = 'none'
        count_click_menu = true
    }
}