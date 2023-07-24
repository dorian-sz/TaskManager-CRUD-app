import React, { useEffect, useState } from 'react'
import "./styles/ProfileCard.css";
import { Fetch } from './Fetch';
import { json } from 'react-router-dom';

const ProfileCard = ({userData}) => {
  const [user, setUser] = useState({});

  const updateUsername = async () => {
    const userDTO = JSON.stringify(user);
    await Fetch(`User`, "PUT", userDTO)
  }
  useEffect(() =>{
    setUser(userData)
  }, [userData])
  return (
    <div className='profile-card-container'>
        <p>User id: {user?.userID}</p>
        <div className='username-container'>
          <p>Username: <input className='username-input' value={user?.username} onChange={e => setUser({...user, username: e.target.value})}></input></p>
          <button className='username-update-btn' onClick={updateUsername}>Update</button>
        </div>
    </div>
  )
}

export default ProfileCard