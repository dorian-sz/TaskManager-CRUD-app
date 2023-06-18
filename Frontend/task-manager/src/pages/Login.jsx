import React, {useRef, useState} from 'react';
import { useEffect } from 'react';
import {Navigate} from 'react-router-dom';

const Login = () => {
    const userRef = useRef();
    const errRef = useRef();

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errMsg, setErrMsg] = useState('')
    const [success, setSuccess] = useState(false);

    useEffect(() => {
        userRef.current.focus();
    }, []);

    useEffect(() => {
        setErrMsg('');
    }, [username, password]);

    const handleSubmit = async (e) => {
        e.preventDefault();

    }

    return (
        <div className="form-signin">
            <p ref={errRef} className={errMsg ? "errmsg" : "offscreen" } aria-live='assertive'>{errMsg}</p>
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
        </div>
    );
};

export default Login;