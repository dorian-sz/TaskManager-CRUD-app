import jwt_decode from "jwt-decode";
import { useState } from 'react';
import { Link, useNavigate } from "react-router-dom";
import "./styles/Form.css"
import useAuth from "../hooks/useAuth";

const loginUrl = "http://localhost:5084/api/Auth"

const Login = () => {
    const { setAuth } = useAuth();

    const navigate = useNavigate();
    const navTo =  "/tasks";

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [displayErr, setDisplayErr] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const response = await fetch(loginUrl, {
            method : "POST",
            headers : {
                "Content-Type" : "application/json"
            },
            credentials : 'include',
            body : JSON.stringify({username, password})
        })
        if (response.ok) {
            const responseJson = await response.json();
            const token = responseJson.token;
            const jwtObj = jwt_decode(token);
            const role = jwtObj.role;
            const userID = jwtObj.userID;
            setAuth({username, password, userID, role, token});
            
            setUsername('');
            setPassword('');
            setDisplayErr(false);
            navigate(navTo, {replace : true});
        }else{
            setDisplayErr(true);
        }
    }

    return (
        <div className="form-container">
            <form onSubmit={handleSubmit} className="form">
                <h3 className="">Sign in</h3>

                <div className="form-input-container">
                    <input type="text" className="form-input" id="username" placeholder="Username"
                        onChange={e => setUsername(e.target.value)} value={username} required
                    />
                </div>
                <div className="form-input-container">
                    <input type="password" className="form-input" id="password" placeholder="Password"
                        onChange={e => setPassword(e.target.value)}
                        value={password} required
                    />
                </div>
                <button className="form-submit-btn" type="submit">SIGN IN</button>
            </form> 
            <h4 className={"" + " " + (displayErr ? "show" : "hide")}>Invalid credentials</h4>
        </div>
    );
};

export default Login;