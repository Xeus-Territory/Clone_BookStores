const sideBarItems = document.querySelectorAll(".sidebar__item");
for (let i = 0; i < sideBarItems.length; i++) {
    sideBarItems[i].addEventListener("click", function () {
        document.querySelector(".sidebar__item.active").classList.remove("active");
        this.classList.add("active");
        if (this.getAttribute("data-tab")) {
            let content = "";
            for (let j = 0; j < this.getAttribute("data-tab").length; j++) {
                if (j === 0) {
                    content += this.getAttribute("data-tab")[j].toUpperCase();
                }
                else content += this.getAttribute("data-tab")[j];
            }
            document.querySelector(".main-header > h2").innerHTML = content;
            document.querySelector(".tab.active").classList.remove("active");
            document.querySelector(`.tab.tab-${this.getAttribute("data-tab")}`).classList.add("active");
        }
    })
}