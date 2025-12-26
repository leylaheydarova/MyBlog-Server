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
