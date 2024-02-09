import { Outlet } from "react-router"
import Navbar from "../components/Navbar/Navbar"

const MainLayout = () => {
  return (
    <div className="flex justify-center w-full min-h-screen bg-zinc-100">
        <div className="w-full md:w-3/5 h-full">
            <Navbar />
            <Outlet />
        </div>
    </div>
  )
}

export default MainLayout