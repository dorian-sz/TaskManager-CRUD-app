import React, { useState } from 'react'
import FloatingLabelInput from './FloatingLabelInput'
import { useLogin } from '../../hooks/useLogin'

const LoginForm = () => {
    const [formData, setFormData] = useState({
        'username': '',
        'password': '',
    })
    const {signIn, error} = useLogin();

    const handleSubmit = async (e) => {
        e.preventDefault();
        const data = JSON.stringify(formData);
        signIn(data);
    }

    return (
        <form onSubmit={handleSubmit} className='flex gap-y-6 flex-col'>
            {error && 
                <div className='w-full border-red-600 rounded-md bg-red-500 p-3 text-white font-semibold'>
                    <p>{error}</p>
                </div>
            }
            <FloatingLabelInput id={"username"} label={"Username"} type={"text"} setFormData={setFormData} formData={formData} isLoading={false}/>
            <FloatingLabelInput id={"password"} label={"Password"} type={"password"} setFormData={setFormData} formData={formData} isLoading={false}/>
            <button type='submit' disabled={false} className='bg-blue-900 rounded-md p-2 text-white font-bold disabled:opacity-75'>Log in</button>
        </form>
    )
}

export default LoginForm