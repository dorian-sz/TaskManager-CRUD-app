import React from 'react';
import {Link} from "react-router-dom";
import "./styles/Navbar.css";

const Navbar = () => {
    return (
        <div>
            <nav className="nav-bar">
                    <ul className="nav-list">
                        <div className="li-container">
                            <li className="">
                                <Link to="/tasks" className="nav-link">Tasks</Link>
                            </li>
                            <li className="">
                                <Link to="/register" className="nav-link">Profile</Link>
                            </li>
                        </div>
                        <div className="li-container">
                            <li className="">
                                <Link to="/login" className="nav-link">Logout</Link>
                            </li>
                        </div>
                    </ul>
            </nav>
        </div>
    );
};

export default Navbar;