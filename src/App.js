import React from 'react'
import { Switch, Route } from 'react-router-dom'

import Navigation from './components/Navigation'
import About from './pages/About'
import Home from './pages/Home'

export default class App extends React.Component {
  render () {
    return (
      <div>
        <Navigation></Navigation>
        <Switch>
          <Route exact path='/' component={Home}></Route>
          <Route exact path='/about' component={About}></Route>
        </Switch>
      </div>
    )
  }
}
