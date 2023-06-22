import React, { useState } from 'react'
import "./styles/Welcome.css"
import Login from '../components/Login';
import Register from '../components/Register';

const Welcome = () => {
    const [displayLogin, setDisplayLogin] = useState(true);

    return (
        <div className='home'>
            <div className="button-container">
                <button className={`tab-btn ${displayLogin ? "tab-btn_clicked" : ""}`} onClick={()=>setDisplayLogin(true)} type="button">LOGIN</button>
                <button className={`tab-btn ${!displayLogin ? "tab-btn_clicked" : ""}`} onClick={()=>setDisplayLogin(false)} type="button">REGISTER</button>
            </div>
            {displayLogin 
            ? <Login/>
            : <Register setDisplay={setDisplayLogin}/>
            }
            
        </div>
    )
}

export default Welcome