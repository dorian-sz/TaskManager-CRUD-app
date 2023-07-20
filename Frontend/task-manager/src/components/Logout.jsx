import { Link } from "react-router-dom";
import useAuth from "../hooks/useAuth";

const Logout = () => {
    const {setAuth} = useAuth({});

    const logout = async () => {

        const response = await fetch("http://localhost:5084/api/Auth/logout", {
            method : 'POST',
            headers : {'Content-Type' : 'application/json'},
            credentials : 'include'
        
        })

        if (response.ok) {
            setAuth({});
        }
    }
    return(
        <Link to="/" className="nav-link" onClick={logout}>Logout</Link>
    )
}

export default Logout;