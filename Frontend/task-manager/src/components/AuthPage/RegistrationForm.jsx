import React, { useState } from 'react'
import FloatingLabelInput from './FloatingLabelInput';
import { useFetch } from '../../hooks/useFetch';

const RegistrationForm = ({setDisplayLogin}) => {
    const [formData, setFormData] = useState({
        'username': '',
        'password': '',
    })
    const {customFetch} = useFetch();
    const [isLoading, setIsLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true);
        try {
            const body= JSON.stringify(formData)
            const response = await customFetch('api/Register',"POST", body);
            if (response.ok) {
                setDisplayLogin(true);
            }
        } catch (error) {
            console.log(error)
        }
        setIsLoading(false);
    }

    return (
        <form onSubmit={handleSubmit} className='flex gap-y-6 flex-col'>
            <FloatingLabelInput id={"username"} label={"Username"} type={"text"} setFormData={setFormData} formData={formData} isLoading={isLoading}/>
            <FloatingLabelInput id={"password"} label={"Password"} type={"password"} setFormData={setFormData} formData={formData} isLoading={isLoading}/>
            <button type='submit' disabled={isLoading} className='bg-blue-900 rounded-md p-2 text-white font-bold disabled:opacity-75'>Register</button>
        </form>
    )
}

export default RegistrationForm