import { useFetch } from './useFetch'

export const useGetTasks = () => {
    const {customFetch} = useFetch();

    const getTasks = async (userId) => {
        const response = await customFetch(`api/Task/userstasks/${userId}`, "GET")
        const fetchedTasks = await response.json();
        return fetchedTasks;
    }

    return {getTasks}
}
