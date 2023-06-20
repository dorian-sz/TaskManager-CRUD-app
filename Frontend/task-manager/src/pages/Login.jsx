import jwt_decode from "jwt-decode";
import { useState } from 'react';
import { Link, useNavigate } from "react-router-dom";
import "./Login.css";
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
            setAuth({username, password, role, token});
            
            setUsername('');
            setPassword('');
            setDisplayErr(false);
            navigate(navTo, {replace : true});
        }else{
            setDisplayErr(true);
        }
    }

    return (
        <div className="form-signin">
            <form onSubmit={handleSubmit}>
                <h1 className="h3 mb-3 fw-normal">Please sign in</h1>

                <div className="form-floating">
                    <input type="text" className="form-control" id="username" placeholder="Username"
                        onChange={e => setUsername(e.target.value)} value={username} required
                    />
                    <label for="floatingInput">Email address</label>
                </div>
                <div className="form-floating">
                    <input type="password" className="form-control" id="password" placeholder="Password"
                        onChange={e => setPassword(e.target.value)}
                        value={password} required
                    />
                    <label for="floatingPassword">Password</label>
                </div>
                <button className="w-100 btn btn-lg btn-primary" type="submit">Sign in</button>
            </form> 
            <h4 className={"text-danger" + " " + (displayErr ? "show" : "hide")}>Invalid credentials</h4>
        </div>
    );
};

export default Login;