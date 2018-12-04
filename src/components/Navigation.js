import React from 'react'
import { connect } from 'react-redux'
import { NavLink } from 'react-router-dom'

class Navigation extends React.Component {
  render () {
    return (
      <nav className='navbar navbar-expand-lg navbar-dark bg-dark'>
        <NavLink to='/' className='navbar-brand'>Forum</NavLink>
        <button className='navbar-toggler' type='button' data-toggle='collapse' data-target='#navbarNav' aria-controls='navbarNav' aria-expanded='false' aria-label='Toggle navigation'>
          <span className='navbar-toggler-icon' />
        </button>
        <div className='collapse navbar-collapse' id='navbarNav'>
          {/* LEFT NAV */}
          <ul className='navbar-nav'>
            <li className='nav-item active'>
              <NavLink to='/' className='nav-link'>Home</NavLink>
            </li>
            <li className='nav-item'>
              <NavLink to='/themes' className='nav-link'>Browse Themes</NavLink>
            </li>
            <li className='nav-item'>
              <NavLink to='/about' className='nav-link'>About</NavLink>
            </li>
          </ul>
          {/* RIGHT NAV */}
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <NavLink to='/signup' className='nav-link'>Sign up</NavLink>
            </li>
            <li className="nav-item">
              <NavLink to='/login' className='nav-link'>Login</NavLink>
            </li>
          </ul>
        </div>
      </nav>
    )
  }
}

export default connect(state => state)(Navigation)
