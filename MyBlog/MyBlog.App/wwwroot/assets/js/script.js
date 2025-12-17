const posts = [
    { id: 1, title: "ASP.NET MVC nədir?", category: "tech", content: "MVC proqramlaşdırmada..." },
    { id: 2, title: "Həyat motivasiyası", category: "life", content: "Motivasiya gündəlik həyatda..." },
    { id: 3, title: "Pankek resepti", category: "food", content: "Ən yüngül pankek resepti..." }
];

const postList = document.getElementById("postList");
const filter = document.getElementById("categoryFilter");

function displayPosts(cat = "all") {
    postList.innerHTML = "";
    posts
        .filter(p => cat === "all" ? true : p.category === cat)
        .forEach(p => {
            postList.innerHTML += `
            <div class="post">
                <h2>${p.title}</h2>
                <p>${p.content.substring(0, 70)}...</p>
                <div class="category">${p.category}</div><br>
                <a class="readmore" href="post.html?id=${p.id}">Read more →</a>
            </div>
        `;
        });
}

displayPosts();
filter.addEventListener("change", () => displayPosts(filter.value));
