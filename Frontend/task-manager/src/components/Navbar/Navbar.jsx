import React from 'react';
import NavItem from './NavItem';
import { useSignout } from '../../hooks/useSignout';

const Navbar = () => {
    const {signOut} = useSignout();

    return (
        <nav className="flex justify-center w-full bg-blue-900 p-4">
                <ul className="flex justify-between w-full md:w-3/4">
                    <div className="flex justify-between w-2/5">
                        <NavItem label={"Tasks"} to={"tasks"} />
                        <NavItem label={"Profile"} to={"profile"}/>
                    </div>
                    <div className="flex">
                        <NavItem label={"Log out"} onclick={signOut}/>
                    </div>
                </ul>
        </nav>
    );
};

export default Navbar;