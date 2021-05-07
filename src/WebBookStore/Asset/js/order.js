const orderContentBoxs = document.querySelectorAll(".order-info > .order-info__item > .order-content-box");

let maxHeight = 0;
orderContentBoxs.forEach(item => {
    maxHeight = maxHeight < item.scrollHeight ? item.scrollHeight : maxHeight
});

orderContentBoxs.forEach(item => {
    item.setAttribute("style", "height: " + maxHeight + "px");
});