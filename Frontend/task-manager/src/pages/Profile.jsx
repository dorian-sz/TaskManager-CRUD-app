import {useEffect, useState} from 'react'
import ProfileCard from '../components/Profile/ProfileCard';
import { useAuthContext } from '../hooks/useAuthContext';

const Profile = () => {
  const [userData, setUserData] = useState({});
  const {user} = useAuthContext();

  useEffect(()=>{
    if (user) {      
      fetch(`${process.env.REACT_APP_API_URL}api/User/${user.userID}`, {
        headers : {
            "Content-Type" : "application/json"
        },
        credentials : 'include',
      })
      .then(response => response.json())
      .then(data => setUserData(data))
    }

  },[user])

  return (
    <div className='w-full h-full overflow-hidden'>
      <ProfileCard userData={userData}></ProfileCard>
    </div>
  )
}

export default Profile