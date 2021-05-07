// ------------------------------KICH HOAT MODAL CHO TRANG USER

const ctrlItemEdit = document.querySelector(".user-ctrl__item.edit");
if (ctrlItemEdit) {
    ctrlItemEdit.addEventListener("click", function () {
        document.querySelector(".modal-user-infor").classList.add("open")
    });
}

//------------------------------KICH HOAT MODAL CHO TRANG ADMIN
//---------Modal Book
const createBookBtn = document.querySelector(".create-btn.newbook");
if (createBookBtn) {
    createBookBtn.addEventListener("click", () => {
        document.querySelector(".modal-book .btn-modal").innerHTML = "Add"
        document.querySelector(".modal-book .modal-header > h2").innerHTML = "Add New Book"
        document.querySelector(".modal-user-infor.modal-book").classList.add("open")
    })
}

const editBookBtn = document.querySelector(".admin-edit-book");
if (editBookBtn) {
    editBookBtn.addEventListener("click", () => {
        document.querySelector(".modal-book .btn-modal").innerHTML = "Update"
        document.querySelector(".modal-book .modal-header > h2").innerHTML = "Update Book"
        document.querySelector(".modal-user-infor.modal-book").classList.add("open")
    })
}
//---------Modal Customer
const createCustomerBtn = document.querySelector(".create-btn.newcustomer");
if (createCustomerBtn) {
    createCustomerBtn.addEventListener("click", () => {
        document.querySelector(".modal-customer .btn-modal").innerHTML = "Add"
        document.querySelector(".modal-customer .modal-header > h2").innerHTML = "Add New Customer"
        document.querySelector(".modal-user-infor.modal-customer").classList.add("open")
    })
}

const editCustomerBtn = document.querySelector(".admin-edit-customer");
if (editCustomerBtn) {
    editCustomerBtn.addEventListener("click", () => {
        document.querySelector(".modal-customer .btn-modal").innerHTML = "Update"
        document.querySelector(".modal-customer .modal-header > h2").innerHTML = "Update Customer"
        document.querySelector(".modal-user-infor.modal-customer").classList.add("open")
    })
}

function close() {
    document.querySelector(".modal-user-infor.open").classList.remove("open")
    if (ctrlItems.length) {
        document.querySelector(".user-ctrl__item.edit").classList.remove("active")
        const ctrlItems = document.querySelectorAll(".user-ctrl__item");
        const tabActive = document.querySelector(".user-tab.active");
        ctrlItems.forEach(ctrlItem => {
            if (ctrlItem.getAttribute("data-tab") == tabActive.getAttribute("data-content")) {
                ctrlItem.classList.add("active");
            }
        });
    }

}

const ctrlCloses = document.querySelectorAll(".close");
const overlayModal = document.querySelector(".overlay");
overlayModal.addEventListener("click", close);
for (let i = 0; i < ctrlCloses.length; i++) {
    ctrlCloses[i].addEventListener("click", close);
}