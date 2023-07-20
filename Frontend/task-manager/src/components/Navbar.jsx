import React from 'react';
import {Link} from "react-router-dom";
import useAuth from '../hooks/useAuth';
import Logout from './Logout';
import "./styles/Navbar.css";

const Navbar = () => {
    const {setAuth} = useAuth({});

    return (
        <div>
            <nav className="nav-bar">
                    <ul className="nav-list">
                        <div className="li-container">
                            <li className="">
                                <Link to="/tasks" className="nav-link">Tasks</Link>
                            </li>
                            <li className="">
                                <Link to="/profile" className="nav-link">Profile</Link>
                            </li>
                        </div>
                        <div className="li-container">
                            <li className="">
                                <Logout/>
                            </li>
                        </div>
                    </ul>
            </nav>
        </div>
    );
};

export default Navbar;