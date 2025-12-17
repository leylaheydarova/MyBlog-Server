document.addEventListener("DOMContentLoaded", () => {

    const user = JSON.parse(localStorage.getItem("user"));

    const navLinks = document.querySelector(".nav-links");

    if (!navLinks) return;

    if (user) {
        // User LOGIN olubsa navbar belə olacaq:
        navLinks.innerHTML = `
            <a href="favorites.html">Favorites</a>
            <a href="profile.html">Profile</a>
            <a href="#" id="logoutBtn">Logout</a>
        `;

        document.getElementById("logoutBtn").addEventListener("click", () => {
            localStorage.removeItem("user");
            window.location.reload();
        });

    } else {
        // User LOGIN olmayıbsa navbar:
        navLinks.innerHTML = `
            <a href="login.html">Login</a>
            <a href="register.html">Register</a>
        `;
    }
});
