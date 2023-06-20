import React, {useState} from 'react';
import {Navigate} from 'react-router-dom';

const Register = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [redirect, setRedirect] = useState('');

    const submit = async (e) => {
        e.preventDefault();

        const response = await fetch("http://localhost:5084/api/Register", {
            method : 'POST',
            headers : {'Content-Type' : 'application/json'},
            body : JSON.stringify({
                username,
                password
            })
        });
        setRedirect(true);
    }

    if (redirect) {  
        return <Navigate to="/login"/>
    }

    return (
        <div className="form-signin">
            <form onSubmit={submit}>
                <h1 className="h3 mb-3 fw-normal">Please register</h1>

                <div className="form-floating">
                    <input type="email" className="form-control" id="floatingInput" placeholder="name@example.com"
                        onChange={e => setUsername(e.target.value)}
                    />
                    <label for="floatingInput">Email address</label>
                </div>
                <div className="form-floating">
                    <input type="password" className="form-control" id="floatingPassword" placeholder="Password"
                        onChange={e => setPassword(e.target.value)}
                    />
                    <label for="floatingPassword">Password</label>
                </div>
                <button className="w-100 btn btn-lg btn-primary" type="submit">Submit</button>
            </form> 
        </div>
    );
};

export default Register;