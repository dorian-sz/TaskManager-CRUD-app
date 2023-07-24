import {useEffect, useState} from 'react'
import useAuth from '../hooks/useAuth';
import { Fetch } from '../components/Fetch';
import ProfileCard from '../components/ProfileCard';

const Profile = () => {
  const [userData, setUserData] = useState();
  const {auth} = useAuth();
  const userID = auth?.userID;

  const GetUserData = async () => {
    const response = await Fetch(`User/${userID}`, "GET")
    const fetchedUser = await response.json();
    setUserData(fetchedUser);
  }

  useEffect(()=>{
    GetUserData();
    return;
  },[])

  return (
    <>
      <ProfileCard userData={userData}></ProfileCard>
    </>
  )
}

export default Profile