import React from 'react'
import { Link } from 'react-router-dom'

const NavItem = ({label, to, onclick}) => {
  return (
    <li onClick={onclick} className="text-white font-semibold text-xl p-2 hover:text-blue-600">
        <Link to={to}>{label}</Link>
    </li>
  )
}

export default NavItem