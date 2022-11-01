var taxRate = 0.05;
var shippingRate = 15.00; 
var fadeTime = 300;
const $ = document.querySelector.bind(document)
const $$ = document.querySelectorAll.bind(document)

/* Assign actions */
$('.product-quantity input').onChange = function (e) {
    updateCartDetail();
    updateQuantity(this);
};

$('.product-removal button').onClick = function () {
    deleteCartDetail();
    removeItem(this);
};


/* Recalculate cart */
function recalculateCart()
{
    var subtotal = 0;
  
    /* Sum up row totals */
    $('.product').each(function () {
        subtotal += parseFloat($(this).children('.product-line-price').text());
    });
  
    /* Calculate totals */
    var tax = subtotal * taxRate;
    var shipping = (subtotal > 0 ? shippingRate : 0);
    var total = subtotal + tax + shipping;
  
    /* Update totals display */
    $('.totals-value').fadeOut(fadeTime, function() {
    $('#cart-subtotal').html(subtotal.toFixed(2));
    $('#cart-tax').html(tax.toFixed(2));
    $('#cart-shipping').html(shipping.toFixed(2));
    $('#cart-total').html(total.toFixed(2));
    if(total == 0){
        $('.checkout').fadeOut(fadeTime);
    }else{
        $('.checkout').fadeIn(fadeTime);
    }
    $('.totals-value').fadeIn(fadeTime);
  });
}


/* Update quantity */
function updateQuantity(quantityInput)
{
    /* Calculate line price */
    var productRow = $(quantityInput).parent().parent();
    var price = parseFloat(productRow.children('.product-price').text());
    var quantity = $(quantityInput).val();
    var linePrice = price * quantity;
  
    /* Update line price display and recalc cart totals */
    productRow.children('.product-line-price').each(function () {
        $(this).fadeOut(fadeTime, function() {
        $(this).text(linePrice.toFixed(2));
        recalculateCart();
        $(this).fadeIn(fadeTime);
        });
    });  
}

// Call API to update number of product in a CartDetail
function updateCartDetail() {
    let number = $('.product-quantity input').value
    let productId = $('.product-details product-id').innerText
    let userId = "05235465-f941-4e00-98bb-5306da1de482"
    fetch("https://localhost:7173/CartDetail/UpdateCartDetail/" + productId + "?userId=" + userId),
    {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
        body: JSON.stringify(number)
    })
    .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Unable to add item.', error));
}


/* Remove item from cart */
function removeItem(removeButton)
{
    /* Remove row from DOM and recalc cart total */
    var productRow = $(removeButton).parent().parent();
    productRow.slideUp(fadeTime, function() {
        productRow.remove();
    recalculateCart();
    });
}

// Call API to delete CartDetail
function deleteCartDetail() {
    let number = $('.product-quantity input').value
    let productId = $('.product-details product-id').innerText
    let userId = "05235465-f941-4e00-98bb-5306da1de482"
    fetch("https://localhost:7173/CartDetail/DeleteCartDetail/" + productId + "?userId=" + userId),
    {
        method: 'POST',
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
        body: JSON.stringify(number)
    })
    .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Unable to add item.', error));
}