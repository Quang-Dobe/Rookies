const $ = document.querySelector.bind(document)
const $$ = document.querySelectorAll.bind(document)

const divToastElement = $('.container #toast')
const AddToCartElement = document.querySelector(".col-lg-7.pb-5 .btn.btn-primary.px-3")


function getNotice(divToastElement, type) {
    // Create toast-div
    let toastElement = document.createElement('div')
    toastElement.classList.add('toast')
    toastElement.classList.add((type === 'success') ? 'toast--success' : 'toast--error')


    // Create and Add child element for toast__icon
    let toast__icon_element = document.createElement('div')
    let icon_toast__icon = document.createElement('i')
    icon_toast__icon.classList.add('fa-regular')
    icon_toast__icon.classList.add('fa-circle-check')
    toast__icon_element.appendChild(icon_toast__icon)


    // Create and Add child elements for toast__body
    let toast__body_element = document.createElement('div')
    let h3_toast__body = document.createElement('h3')
    h3_toast__body.classList.add('toast__content')
    h3_toast__body.innerText = (type === 'success') ? "Success" : "Error"
    toast__body_element.appendChild(h3_toast__body)
    let p_toast__body = document.createElement('p')
    p_toast__body.classList.add('toast__msg')
    p_toast__body.innerText = (type === 'success') ? "Buy successfully" : "Sorry. There is something went wrong!"
    toast__body_element.appendChild(p_toast__body)


    // Create and Add child element for toast__exit
    let toast__exit_element = document.createElement('div')
    let icon_toast__exit = document.createElement('i')
    icon_toast__exit.classList.add('fa-regular')
    icon_toast__exit.classList.add('fa-circle-xmark')
    toast__exit_element.appendChild(icon_toast__exit)

    toast__exit_element.onclick = function (e) {
        divToastElement.removeChild(toastElement)
    }


    toastElement.appendChild(toast__icon_element)
    toastElement.appendChild(toast__body_element)
    toastElement.appendChild(toast__exit_element)

    divToastElement.appendChild(toastElement)

    setTimeout(function () {
        divToastElement.removeChild(toastElement)
    }, 4010)
}

const userAction = async () => {
    var number = $('.input-group.quantity input').value
    var productId = $('.col-lg-7 pb-5 p#productId').innerText
    var userId = "05235465-f941-4e00-98bb-5306da1de482";

    fetch("https://localhost:7173/CartDetail/CreateCartDetail/" + productId + "?userId=" + userId,
        {
            method: 'POST',
            headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
            body: JSON.stringify(number)
        })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Unable to add item.', error));
}

AddToCartElement.onclick = async function (e) {
    await userAction();
    getNotice(divToastElement, 'success')
}