function getAllData(url, callback) {
    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {
            callback(null, data);
        })
        .catch(error => {
            callback(error, null);
        });
}

function getAllDataById(url, id, callback) {
    // Append the ID to the URL if it's provided
    // const apiUrl = id ? `${url}?id=${id}` : url;
    debugger
    const apiUrl = id ? `${url}/${id}` : url;

    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            return response.text();
        })
        .then(data => {
            callback(null, data);
        })
        .catch(error => {
            callback(error, null);
        });
}


function getActionById(url, id, callback) {
    // Append the ID to the URL if it's provided
    // const apiUrl = id ? `${url}?id=${id}` : url;
    debugger
    const apiUrl = id ? `${url}/${id}` : url;

    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {
            callback(null, data);
        })
        .catch(error => {
            callback(error, null);
        });
}


/*function checkResult(error, data) {
    console.log(data + "dataaaa")
    if (error == null) return data
    return error

}
let result = getAllData('AboutCompany/GetAllAboutcompany', checkResult);
console.log(result + "Result");
*/



