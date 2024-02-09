import {useState} from 'react'
import { useAuthContext } from './useAuthContext'
import { useNavigate } from 'react-router-dom';
import { useFetch } from './useFetch';

export const useLogin = () => {
    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState(null);
    const navigate = useNavigate();
    const { dispatch } = useAuthContext();
    const {customFetch} = useFetch();

    const signIn = async (formData) =>{
        setIsLoading(true);
        setError(null);
        try {
            const response = await customFetch('api/Auth', 'POST', formData);
            if (response.ok) {
                const data = await response.json();
                const user = data.user;
                const token = data.token;
                localStorage.setItem('user', JSON.stringify({...user, ...token}));
                dispatch({type: 'LOGIN', payload: {...user, ...token}});
                navigate('/tasks')
                setIsLoading(false)
            }else{
                setError("Invalid credentials.");
                setIsLoading(false);
            }
        } catch (err) {
            console.log(err);
            setIsLoading(false)
        }

    }
    return {signIn, error, isLoading}
}
