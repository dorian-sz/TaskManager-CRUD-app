import React, { useState } from 'react'
import LoginForm from '../components/AuthPage/LoginForm'
import { twMerge } from 'tailwind-merge'
import RegistrationForm from '../components/AuthPage/RegistrationForm';

const AuthenticationPage = () => {
    const [displayLogin, setDisplayLogin] = useState(true);

    return (
        <div className='flex w-full h-full justify-center items-center'>
            <div className='flex w-3/4 md:w-1/2 h-1/2 md:h-3/4 border md:bg-blue-900 rounded-md'>
                <div className= 'hidden md:flex w-2/5 min-h-full'>

                </div>
                <div className='flex flex-col relative justify-center bg-white items-center w-full md:ml-auto md:w-3/5'>
                    <div className='flex absolute top-0 w-full h-16 border-b items-end'>
                        <div className='flex w-full justify-center gap-x-8'>
                            <div onClick={() => setDisplayLogin(true)}  className={twMerge('flex p-2 w-1/2 md:w-1/5 justify-center border-b-2 border-b-transparent cursor-pointer', displayLogin ? 'border-b-blue-900' : '')}>
                                <p className='font-semibold uppercase text-gray-600'>Log in</p>
                            </div>
                            <div onClick={() => setDisplayLogin(false)}  className={twMerge('flex p-2 w-1/2 md:w-1/5 justify-center border-b-2 border-b-transparent cursor-pointer', !displayLogin ? 'border-b-blue-900' : '')}>
                                <p className='font-semibold uppercase text-gray-600'>Register</p>
                            </div>
                        </div>
                    </div>
                    <div className='w-3/4 md:w-1/2'>
                        {displayLogin && <LoginForm />}
                        {!displayLogin && <RegistrationForm setDisplayLogin={setDisplayLogin} />}
                    </div>
                </div>
            </div>
        </div>
    )
}

export default AuthenticationPage