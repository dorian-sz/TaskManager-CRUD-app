import React, {useState} from 'react';
import {Navigate} from 'react-router-dom';
import "./styles/Form.css";

const Register = ({setDisplay}) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        const response = await fetch("http://localhost:5084/api/Register", {
            method : 'POST',
            headers : {'Content-Type' : 'application/json'},
            body : JSON.stringify({
                username,
                password
            })
        });
        setDisplay(true);
    }

    return (
        <div className="form-container">
            <form onSubmit={handleSubmit} className="form">
                <h3 className="">Sign up</h3>

                <div className="form-input-container">
                    <input type="text" className="from-input" id="username" placeholder="Username"
                        onChange={e => setUsername(e.target.value)} value={username} required
                    />
                </div>
                <div className="form-input-container">
                    <input type="password" className="from-input" id="password" placeholder="Password"
                        onChange={e => setPassword(e.target.value)}
                        value={password} required
                    />
                </div>
                <button className="form-submit-btn" type="submit">SIGN UP</button>
            </form> 
        </div>
    );
};

export default Register;