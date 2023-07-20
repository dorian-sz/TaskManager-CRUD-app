import { Link } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import { Fetch } from "./Fetch";

const Logout = () => {
    const {setAuth} = useAuth({});

    const logout = async () => {
        const response = await Fetch("Auth/logout", "POST")

        if (response.ok) {
            setAuth({});
        }
    }
    return(
        <Link to="/" className="nav-link" onClick={logout}>Logout</Link>
    )
}

export default Logout;