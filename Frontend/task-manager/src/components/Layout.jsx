import {Outlet} from 'react-router-dom'
import Navigation from './Navigation'

const Layout = () => {
  return (
    <div className='App'>
        <Navigation/>
        <Outlet/>
    </div>
  )
}

export default Layout