

const showRegister = () => {
    const registerDiv = document.querySelector(".registerHidden")

    registerDiv.classList.remove("registerHidden")

    registerDiv.classList.add("register")

}

const addUser = async  () => {
    const user = newRegister()
    if (user) {
        try {
            const responsePost = await fetch('https://localhost:7057/api/Users', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(user)

            })

            const dataPost = await responsePost.json()
            if (responsePost.ok) {
                alert("Add user sucsessfully")
                SaveStorege(dataPost)
                
            }
              
            /*alert(dataPost)*/
            else {
                if (responsePost.status == 400)
                    alert("חוזק סיסמא לא תקין") 
            }
        }
        catch (err) {
            alert(err)
        }
    }

  
}

const getPassword = () => {
    const password = document.querySelector("#password").value
    return password;
}
const updateLevel = (dataPost) => {
    const level = document.querySelector("#level")
    console.log(level.value)
    level.value = dataPost
    console.log(level.value)
    
}


const cheakPassword = async () => {
    const password = getPassword()
    try {
        const responsePost = await fetch('https://localhost:7057/api/Users/password', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },

            body: JSON.stringify(password)

        })

        const dataPost = await responsePost.json()
        alert(dataPost)
        updateLevel(dataPost)
    }
    catch {
        alert(err)
    }
}


const newRegister = () => {
    const email = document.querySelector("#email").value

    const password = document.querySelector("#password").value

    const firstName = document.querySelector("#firstName").value

    const lastName = document.querySelector("#lastName").value

    if (email.indexOf('@') == -1 || email.indexOf('.') == -1) { 
        alert("Field email must include @ and .")
         return
}
    if (firstName.length < 2 || firstName.length > 20 || lastName.length < 2 || lastName.length > 20) {
        alert("Name can be between 2 till 20 letters")
        return
    }
    else if (!email || !password || !firstName || !lastName) {
        alert("All field are required")
        return
    }
    else
        return ({ email, password, firstName, lastName })
    //const user = {email, password, firstName, lastName }

    //return user
}


const userLogin = () => {
    const email = document.querySelector("#emailLogin").value
    const password = document.querySelector("#passwordLogin").value

    const user = {email, password}
    return user
}

const fetchLogin = async () => {
    const user = userLogin()

    try {
        const responsePost = await fetch(`https://localhost:7057/api/Users/login/?email=${user.email}&password=${user.password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            query: {
                email: user.email,
                password: user.password
            }
           

        })
       
        const postData = await responsePost.json()
        if (responsePost.ok) {
            console.log('post data:', postData)
            SaveStorege(postData)
            //sessionStorage.setItem("Id", postData.id)
            //sessionStorage.setItem("Email", postData.email)
            // //sessionStorage.setItem("Id", postData.lastName)
            //sessionStorage.setItem("FirstName", postData.firstName)
            //sessionStorage.setItem("LastName", postData.lastName)
            /*window.location.href = "userDetails.html"*/
        }
        else {
            alert("שדות חובה")
        }
    }
    catch (err) {
        alert("משתמש לא קיים במערכת")
    }

}
const SaveStorege = (postData) => {
    sessionStorage.setItem("Id", postData.id)
    sessionStorage.setItem("Email", postData.email)
    //sessionStorage.setItem("Id", postData.lastName)
    sessionStorage.setItem("FirstName", postData.firstName)
    sessionStorage.setItem("LastName", postData.lastName)
    window.location.href = "Products.html"
}