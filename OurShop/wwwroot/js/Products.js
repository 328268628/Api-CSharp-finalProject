const getDataFromForm=async()=>{
    const minPrice = document.querySelector("#minPrice").value;
    const maxPrice = document.querySelector("#maxPrice").value;
    const nameSearch = document.querySelector("#nameSearch").value;
    const categoryIds= sessionStorage.getItem("category")
    return { nameSearch, minPrice, maxPrice, categoryIds }
}
addEventListener("load", () => {

    let product = JSON.parse(sessionStorage.getItem("shopingBag")) || []
    let category =  []
    sessionStorage.setItem("category", JSON.stringify(category))
    sessionStorage.setItem("shopingBag", JSON.stringify(product))
    console.log(product.length);
    document.querySelector("#ItemsCountText").textContent = product.length
    drawCategory()
      GetProducts()
  
}
)
const reset = () => {
    sessionStorage.setItem("category", [])
    sessionStorage.setItem("shopingBag", [])
    GetProducts()
}
const GetProducts = async  () => {
    data =await getDataFromForm();
    //let { nameSearch, minPrice, maxPrice, categoryIds } = await getData()
    categoryIds = JSON.parse(data.categoryIds)
    console.log(categoryIds.length);
    console.log(categoryIds.length);
    let url = `https://localhost:7057/api/Product/`;
    if (data.nameSearch || data.maxPrice || data.minPrice || categoryIds.length != 0) {
        url += '?'
        if (data.nameSearch)
            url += `&desc=${data.nameSearch}`
        if (data.maxPrice)
            url += `&maxPrice=${data.maxPrice}`
        if (data.minPrice)
            url += `&minPrice=${data.minPrice}`
        if (categoryIds.length != 0)
            for (let i = 0; i < categoryIds.length; i++)
                url += `&categoryIds=${categoryIds[i]}`
    }
    try {
        const Product = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                nameSearch: data.nameSearch,
                maxPrice: data.maxPrice,
                minPrice: data.minPrice,
                categoryIds: data.categoryIds
            }
        });
        if (Product.status == 204)
            alert("not found products")
        if (!Product.ok) {
            throw new Error(`HTTP error! status:${response.status}`)
        }
        const dataPost = await Product.json();
        console.log(dataPost);
        document.getElementById("counter").textContent = dataPost.length
        console.log(document.getElementById("counter").textContent);
        drawProducts(dataPost);
    }
    catch (error) {
        alert("אין על לייקי ובתיה בכל העולם!!!!!!!!!!!!!!!!!")
    }

    }

const DrawProduct = (product) => {
    let tmp = document.getElementById("temp-card");
    let productChild = tmp.content.cloneNode(true)
    productChild.querySelector("img").src = "./pictures/" + product.image +'.JPEG'
    productChild.querySelector("h1").textContent = product.name
    productChild.querySelector(".price").innerText = product.price
    productChild.querySelector(".description").innerText = product.description
    productChild.querySelector(".bag").addEventListener("click",()=> { addToCart(product) })
    document.getElementById("PoductList").appendChild(productChild)
}
const drawProducts = async (products) => {
    document.querySelector("#PoductList").innerHTML = " "
    for (let i = 0; i < products.length; i++) {
        DrawProduct(products[i])
    }
}

const drawCategory = async () => {
    category = await addCategory()
    for (let i = 0; i < category.length; i++) {
        let tmp = document.getElementById("temp-category");
        let cloneCategory = tmp.content.cloneNode(true)
        cloneCategory.querySelector("input").id = i
        cloneCategory.querySelector("input").addEventListener('change', () => filterCategory(category[i], i) )
        cloneCategory.querySelector("label").textContent = category[i].name

        document.getElementById("categoryList").appendChild(cloneCategory)
    }
}
const filterCategory = (category, index) => {
    const input = document.getElementById(index)

    if (input.checked) {
        let categories = sessionStorage.getItem("category")
        categories = JSON.parse(categories)
        categories.push(category.id)
        sessionStorage.setItem("category", JSON.stringify(categories))
    }
    else {
        let categories = sessionStorage.getItem("category")
        categories = JSON.parse(categories)
        let i = 0;
        for (; i < categories.length; i++) {
            if (categories[i] == category.id)
                break;
        }
        categories.splice(i, 1);
        sessionStorage.setItem("category", JSON.stringify(categories))
    }
    GetProducts()
}
const addCategory = async () => {
    try {
        const responsePost = await fetch('https://localhost:7057/api/Category', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },

        });
        if (responsePost.status == 204)
            alert("not found categories")
        if (!responsePost.ok) {
            throw new Error(`HTTP error! status:${response.status}`)
        }
        const dataPost = await responsePost.json();
        console.log(dataPost);
        return (dataPost);

    }
    catch (error) {

        alert("אין על לייקי ובתיה בכל העולם!!!!!!!!!!!!!!!!!")
    }



}
const addToCart = (product) => {
    let products = sessionStorage.getItem("shopingBag")
    products = JSON.parse(products)
    products.push(product)
    sessionStorage.setItem("shopingBag", JSON.stringify(products))
    document.querySelector("#ItemsCountText").textContent = parseInt(document.querySelector("#ItemsCountText").textContent) + 1
}



