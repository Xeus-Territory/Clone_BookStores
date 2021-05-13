//----------------------------HEADER-FIX-----------------------------------
const header = document.querySelector("header");
const carousel = document.querySelector(".carousel");
if (carousel) {
    carousel.setAttribute("style", "padding-top: " + header.offsetHeight + "px");
}

const search = document.querySelector(".search");
if (search) {
    search.setAttribute("style", "padding-top: " + header.offsetHeight*2 + "px");
}

const userInfo = document.querySelector(".user-infor");
if (userInfo) {
    userInfo.setAttribute("style", "padding-top: " + header.offsetHeight * 2 + "px");
}

const order = document.querySelector(".order");
if (order) {
    order.setAttribute("style", "padding-top: " + header.offsetHeight * 1.5 + "px");
}

const shoppingCart = document.querySelector(".shopping-cart");
if (shoppingCart) {
    shoppingCart.setAttribute("style", "padding-top: " + header.offsetHeight*1.5 + "px");
}

let currentScrollY = 0;
window.addEventListener("scroll", () => {
    let y = window.scrollY;
    if (y === 0) {
        header.className = '';
    }
    else if (y > currentScrollY) {
        //header.style.position = "fixed";
        header.classList.remove("header-down");
        header.classList.add("header-up");
    }
    else if (y < currentScrollY) {
        //header.style.position = "fixed";
        header.classList.remove("header-up");
        header.classList.add("header-down");
    }
    currentScrollY = y;
})

const quantitys = document.querySelectorAll(".cart__item-quantity > p");
const increaseBtns = document.querySelectorAll(".btn.cart-btn-increase");
const decreaseBtns = document.querySelectorAll(".btn.cart-btn-decrease");
console.log(increaseBtns)
for (let i = 0; i < increaseBtns.length; i++) {
    increaseBtns[i].addEventListener("click", () => {
        console.log(i);
        quantitys[i].innerHTML = Number(quantitys[i].innerHTML) + 1;
    })
}

for (let i = 0; i < increaseBtns.length; i++) {
    decreaseBtns[i].addEventListener("click", () => {
        if (Number(quantitys[i].innerHTML) > 1) {
            quantitys[i].innerHTML = Number(quantitys[i].innerHTML) - 1;
        }
    })
}

// -------------------------PRODUCT-ITEM-----------------------------

const bookNames = document.querySelectorAll(".book-name");
bookNames.forEach((bookName) => {
    if (bookName.innerHTML.length > 35) {
        bookName.innerHTML = bookName.innerHTML.substring(0, 35) + "...";
    }
});

const bookNamess = document.querySelectorAll(".name");
bookNamess.forEach((bookName) => {
    if (bookName.innerHTML.length > 35) {
        bookName.innerHTML = bookName.innerHTML.substring(0, 35) + "...";
    }
});

const prices = document.querySelectorAll(".price-number");
prices.forEach(price => {
    price.innerHTML = Number(price.innerHTML).toLocaleString("en-US");
})








