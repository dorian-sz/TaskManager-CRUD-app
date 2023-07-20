const url = "http://localhost:5084/api/";

export const Fetch = async (endpoint, fetchMethod, fetchBody) => {
    const response = await fetch(`${url}${endpoint}`, {
        method : fetchMethod,
        headers : {
            "Content-Type" : "application/json"
        },
        credentials : 'include',
        body : fetchBody
    })

    return response;
}