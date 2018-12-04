import React from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router-dom'

class Navigation extends React.Component {
  render () {
    return (
      <nav className='navbar navbar-expand-lg navbar-dark bg-dark'>
        <Link to='/' className='navbar-brand'>Forum</Link>
        <button className='navbar-toggler' type='button' data-toggle='collapse' data-target='#navbarNav' aria-controls='navbarNav' aria-expanded='false' aria-label='Toggle navigation'>
          <span className='navbar-toggler-icon' />
        </button>
        <div className='collapse navbar-collapse' id='navbarNav'>
          {/* LEFT NAV */}
          <ul className='navbar-nav'>
            <li className='nav-item active'>
              <Link to='/' className='nav-link'>Home</Link>
            </li>
            <li className='nav-item'>
              <Link to='/themes' className='nav-link'>Browse Themes</Link>
            </li>
            <li className='nav-item'>
              <Link to='/about' className='nav-link'>About</Link>
            </li>
          </ul>
          {/* RIGHT NAV */}
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <Link to='/signup' className='nav-link'>Sign up</Link>
            </li>
            <li className="nav-item">
              <Link to='/login' className='nav-link'>Login</Link>
            </li>
          </ul>
        </div>
      </nav>
    )
  }
}

export default connect(state => state)(Navigation)
