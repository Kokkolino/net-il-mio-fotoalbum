//html cards
const cardIndex = photo => `
    <div class="card bg-white bg-opacity-25 text-white border-dark px-0 mb-4 col-sm-12" style="width: 18rem;">
    <img src="${photo.src}" class="card-img-top" alt="...">
    <div class="card-body">
        <h5 class="card-title">${photo.title}</h5>
        <p class="card-text">${photo.description}</p>
    </div>
    <div id="tags${photo.id}" class="card-body">
    </div>
    <div class="card-body">
        <a href="/User/Details/${photo.id}" class="btn btn-primary">Dettagli</a>
    </div>
</div>`;

const cardDetails = photo => `<h2 class="text-center mb-3 text-white my-5">${photo.title}</h2>
    <img class="container-fluid" src="${photo.src}" />

    <div class="px-5 mt-3 d-flex justify-content-between gap-3">
        <div class="container">
            <span class="text-white-50">Description</span>
            <p class="text-white">${photo.description}</p>
        </div>
        <div class="container">
                <span class="text-white-50">Tags</span>
            <p id="tags" class="text-white"></p>

        </div>

    </div>`;

//Index
const initIndex = filter => getPhotos(filter)
    .then(photos => {
        renderPhotos(photos);
    });

const getPhotos = title => axios
    .get('/api/photo', title ? { params: { title } } : {})
    .then(res => res.data).catch(err => err);

const renderPhotos = photos => {
    const table = document.getElementById("table");
    table.innerHTML = photos.map(cardIndex).join('');
    for (i = 0; i < photos.length; i++) {

        var str = `tags${photos[i].id}`
        const tableTags = document.getElementById(`${str}`)
        const tags = photos[i].tags
        tableTags.innerHTML = tags.map(appendTags).join(", ")

    }

}

//Details
function initDetails() {
    var id = Number(location.pathname.split("/")[3])
    getPhoto(id).then(photo => renderPhoto(photo))
}


const getPhoto = id => axios.get(`/api/photo/${id}`).then(res => res.data)

const renderPhoto = photo => {
    const table = document.getElementById("table");
    table.innerHTML = cardDetails(photo);
    const tableTags = document.getElementById("tags")
    const tags = photo.tags
    tableTags.innerHTML = tags.map(appendTags).join(", ")
}

//Message
const initForm = () => {
    const form = document.getElementById("form");
    form.addEventListener("submit", e => {
        e.preventDefault();

        const message = getMessage(form);
        postMessage(message)
    });
}

const getMessage = form => {
    const name = form.querySelector("#name").value;
    const surname = form.querySelector("#surname").value;
    const email = form.querySelector("#email").value;
    const text = form.querySelector("#text").value;

    return {
        name,
        surname,
        email,
        text
    }
}

const postMessage = message => axios.post("/api/message", message)
    .then(() => location.href = "/user/index")
    .catch(err => console.log(err));

const appendTags = tag => `<a href="#" class="text-white">#${tag.name}</a>`;