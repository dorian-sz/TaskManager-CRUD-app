import {Outlet} from 'react-router-dom'
import Navbar from './Navbar'
import useAuth from "../hooks/useAuth";
const Layout = () => {
  const {auth} = useAuth();
  return (
    
      auth?.username ?
      <div className='App'>
        <Navbar/>
        <Outlet/>
      </div>
      :
      <div className='App'>
        <Outlet/>
      </div>
  )
}

export default Layout