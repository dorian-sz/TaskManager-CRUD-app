import React, { useEffect, useState } from 'react'
import { Navigate, Outlet } from 'react-router-dom'
import { useAuthContext } from '../hooks/useAuthContext';
import Navbar from '../components/Navbar/Navbar';

const AuthLayout = ({ redirectPath = '/', children}) => {
    const [isLoading, setIsLoading] = useState(true);
    const {user} = useAuthContext();
    
    useEffect(()=>{
        if (user || isLoading) {
        setIsLoading(false);
        }
    },[user, isLoading])

    if (isLoading) {
        return null;
    }

    if (!user) {
        return <Navigate to={redirectPath} replace/>
    }

    return (
        children ? children : 
        <div className="flex justify-center w-full h-full">
            <div className='flex flex-col items-center w-full md:w-1/2 h-full bg-zinc-50'>
            <Navbar />
            <Outlet />
            </div>
        </div>
    )
}

export default AuthLayout