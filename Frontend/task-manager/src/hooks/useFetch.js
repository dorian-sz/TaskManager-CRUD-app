export const useFetch = () => {

    const customFetch = async (endpoint, fetchMethod, fetchBody) => {
        const response = await fetch(`${process.env.REACT_APP_API_URL}${endpoint}`, {
            method : fetchMethod,
            headers : {
                "Content-Type" : "application/json"
            },
            credentials : 'include',
            body : fetchBody
        })
    
        return response;
    }

    return {customFetch}
}
