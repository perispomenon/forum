import React from 'react'
import { NavLink } from 'react-router-dom'
// import { connect } from 'react-redux'
// import autoBind from 'react-autobind'

class Home extends React.Component {
  render () {
    return (
      <div className="container">
        <div className="jumbotron">
          <h1 className="display-4">Hello, world!</h1>
          <p className="lead">This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.</p>
          <hr className="my-4"/>
          <p>It uses utility classNamees for typography and spacing to space content out within the larger container.</p>
          <NavLink to='about' className="btn btn-primary btn-lg" role="button">Learn more</NavLink>
        </div>
      </div>
    )
  }
}

export default Home
