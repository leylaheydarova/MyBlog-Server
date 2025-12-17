// REGISTER
function register() {
    const email = document.getElementById("regEmail").value;
    const user = document.getElementById("regUser").value;
    const pass = document.getElementById("regPass").value;

    localStorage.setItem("user", JSON.stringify({ email, user, pass }));

    alert("Qeydiyyat uğurludur!");
    window.location = "login.html";
}

// LOGIN
function login() {
    const email = document.getElementById("loginUser").value;
    const pass = document.getElementById("loginPass").value;

    const saved = JSON.parse(localStorage.getItem("user"));

    if (!saved) return alert("İstifadəçi yoxdur!");

    if ((email === saved.email || email === saved.user) && pass === saved.pass) {
        alert("Uğurla daxil oldun!");
        window.location = "index.html";
    } else {
        alert("Email və ya şifrə səhvdir!");
    }
}

// PROFILE
if (document.getElementById("profUser")) {
    const user = JSON.parse(localStorage.getItem("user"));
    if (user) {
        document.getElementById("profUser").innerHTML = user.user;
        document.getElementById("profEmail").innerHTML = user.email;
    }
}

// LOGOUT
function logout() {
    localStorage.removeItem("user");
    alert("Çıxış edildi.");
    window.location = "index.html";
}
