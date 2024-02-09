import { useNavigate } from 'react-router-dom';
import { useAuthContext } from './useAuthContext';
import { useFetch } from './useFetch';

export const useSignout = () => {
    const { dispatch } = useAuthContext();
    const navigate = useNavigate();
    const {customFetch} = useFetch();

    const signOut = async () =>{
        const response = await customFetch("api/Auth/logout", "POST")
        if (response.ok) {
            dispatch({type: 'LOGOUT'});
            navigate('/');
        }
    }
    return {signOut}
}
