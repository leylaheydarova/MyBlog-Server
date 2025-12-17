// Fake blog data
const posts = [
    { id: 1, title: "MVC nədir?", content: "MVC – Model View Controller arxitekturasıdır..." },
    { id: 2, title: "Motivasiya", content: "Motivasiya həyatda irəli getmək üçün..." },
    { id: 3, title: "Pankek resepti", content: "Un, yumurta, süd..." }
];

// Bloqları homepage-də göstər
if (document.getElementById("blogList")) {
    let html = "";
    posts.forEach(p => {
        html += `
            <div class="blog-card">
                <h3>${p.title}</h3>
                <p>${p.content.substring(0, 60)}...</p>

                <a href="post.html?id=${p.id}" class="btn">Read</a>
                <span class="heart" onclick="addToFav(${p.id})">♥</span>
            </div>
        `;
    });
    document.getElementById("blogList").innerHTML = html;
}

// Post detail
if (window.location.href.includes("post.html")) {
    const q = new URLSearchParams(window.location.search);
    const id = q.get("id");
    const post = posts.find(x => x.id == id);

    if (post) {
        document.getElementById("postTitle").innerHTML = post.title;
        document.getElementById("postContent").innerHTML = post.content;
    }
}

// FAVORITES
function addToFav(id) {
    if (!localStorage.getItem("user")) {
        alert("Sevimlilərə əlavə etmək üçün login olun!");
        return;
    }

    let fav = JSON.parse(localStorage.getItem("fav") || "[]");

    if (!fav.includes(id)) fav.push(id);

    localStorage.setItem("fav", JSON.stringify(fav));

    alert("Sevimlilərə əlavə olundu!");
}

// FAVORITES PAGE
if (document.getElementById("favList")) {
    let fav = JSON.parse(localStorage.getItem("fav") || "[]");
    let html = "";

    fav.forEach(id => {
        const p = posts.find(x => x.id === id);
        html += `
            <div class="fav-card">
                <h3>${p.title}</h3>
                <p>${p.content.substring(0,60)}...</p>
            </div>
        `;
    });

    document.getElementById("favList").innerHTML = html;

    function addComment() {
    const text = document.getElementById("commentInput").value;
    if (!text.trim()) return;

    document.getElementById("commentList").innerHTML += `
        <div class="commentItem">${text}</div>
    `;
    document.getElementById("commentInput").value = "";
}
}
