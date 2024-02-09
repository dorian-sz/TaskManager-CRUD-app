import React, { useEffect, useState } from 'react'
import { useFetch } from '../../hooks/useFetch';
import TaskInput from '../Task/TaskInput';
import TaskButton from '../Task/TaskButton';

const ProfileCard = ({userData}) => {
  const [user, setUser] = useState({});
  const {customFetch} = useFetch();

  const updateUsername = async () => {
    const userDTO = JSON.stringify(user);
    await customFetch(`api/User`, "PUT", userDTO)
  }

  useEffect(() =>{
    setUser(userData)
  }, [userData])
  return (
    <div className='flex flex-col mt-5 border-y-2 gap-y-4 w-full h-full items-center'>
      <div className='flex items-center w-full border-b-2 gap-x-4 border-dashed p-4'>
        <p className='text-2xl italic font-semibold'>User id:</p>
        <p className='text-2xl italic font-semibold'>{user?.userID}</p>
      </div>        
      <div className='flex items-center w-full border-b-2 gap-x-4 border-dashed p-4'>
        <p className='text-2xl italic font-semibold'>Username:</p>
        <TaskInput object={user} value={user?.username} setValue={setUser} id={"username"}/>
      </div>
      <div className='w-1/5'>
        <TaskButton label={"Update"} className={"bg-blue-900 w-full hover:bg-blue-950"} onclick={updateUsername} />
      </div>
    </div>
  )
}

export default ProfileCard