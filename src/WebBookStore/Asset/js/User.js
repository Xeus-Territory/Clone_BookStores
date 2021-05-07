const ctrlItems = document.querySelectorAll(".user-ctrl__item");
if (ctrlItems) {
    for (let i = 0; i < ctrlItems.length; i++) {
        ctrlItems[i].addEventListener("click", function () {
            document.querySelector(".user-ctrl__item.active").classList.remove("active");
            this.classList.add("active");
            if (this.getAttribute("data-tab")) {
                document.querySelector(".user-tab.active").classList.remove("active");
                document.querySelector(`.user-tab.tab-${this.getAttribute("data-tab")}`).classList.add("active");
            }
        })
    }
}