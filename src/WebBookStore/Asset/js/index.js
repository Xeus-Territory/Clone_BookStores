const switchRegister = document.querySelector('.switch-link.switch-register');
const switchLogin = document.querySelector('.switch-link.switch-login');
const formLogin = document.querySelector('.form-login');
const formRegister = document.querySelector('.form-register');

//------------------------------Product-Item

const bookNames = document.querySelectorAll(".book-name");
bookNames.forEach((bookName) => {
    if (bookName.innerHTML.length > 20) {
        bookName.innerHTML = bookName.innerHTML.substring(0, 35) + "...";
    }
});


switchRegister.addEventListener('click', (e) => {
    e.preventDefault();
    formRegister.style.transform = 'translateX(-400px)';
    formLogin.style.transform = 'translateX(-400px)';
})

switchLogin.addEventListener('click', (e) => {
    e.preventDefault();
    formRegister.style.transform = 'translateX(0)';
    formLogin.style.transform = 'translateX(0)';
})


