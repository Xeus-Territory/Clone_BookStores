const sectionBook = document.querySelector(".book");
if(sectionBook) {
    sectionBook.setAttribute("style", "padding-top: " + header.offsetHeight*2 + "px");
}


const inputNumber = document.querySelector(".numeric > input");
const increaseBtn = document.querySelector(".numeric__button.numberic__increase");
const decreaseBtn = document.querySelector(".numeric__button.numberic__decrease");
increaseBtn.addEventListener("click", () => {
    inputNumber.value = Number(inputNumber.value) + 1;
})

decreaseBtn.addEventListener("click", () => {
    if(Number(inputNumber.value) > 1) {
        inputNumber.value = Number(inputNumber.value) - 1;
    }  
})