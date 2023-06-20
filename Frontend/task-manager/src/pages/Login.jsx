import React, {useRef, useState} from 'react';
import { useEffect } from 'react';
import "./Login.css";
const loginUrl = "http://localhost:5084/api/Auth"

const Login = () => {
    const userRef = useRef();
    const errRef = useRef();

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [displayErr, setDisplayErr] = useState(false);
    const [success, setSuccess] = useState(false);

    useEffect(() => {
        userRef.current.focus();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(loginUrl, {
                method : "POST",
                headers : {
                    "Content-Type" : "application/json"
                },
                credentials : 'include',
                body : JSON.stringify({username, password})
            })
            const jsonData = await response.json();
            setUsername('');
            setPassword('');
            setSuccess(response.ok);
            setDisplayErr(!response.ok);
        } catch (error) {
            console.log(error);
        }
    }

    return (
        <div className="form-signin">
            <form onSubmit={handleSubmit}>
                <h1 className="h3 mb-3 fw-normal">Please sign in</h1>

                <div className="form-floating">
                    <input type="text" className="form-control" id="username" placeholder="Username" ref={userRef}
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