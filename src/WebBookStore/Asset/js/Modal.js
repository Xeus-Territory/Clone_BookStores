//------------------------------KICH HOAT MODAL CHO TRANG ADMIN
//---------Modal Book
const createBookBtn = document.querySelector(".create-btn.newbook");
if (createBookBtn) {
    createBookBtn.addEventListener("click", () => {
        document.querySelector(".modal-user-infor.modal-book").classList.add("open")
    })
}


function close() {
    document.querySelector(".modal-user-infor.open").classList.remove("open")
}

const ctrlCloses = document.querySelectorAll(".close");
const overlayModal = document.querySelector(".overlay");
overlayModal.addEventListener("click", close);
for (let i = 0; i < ctrlCloses.length; i++) {
    ctrlCloses[i].addEventListener("click", close);
}