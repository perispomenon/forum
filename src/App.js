import React from 'react'
import { Switch, Route } from 'react-router-dom'

import Navigation from './components/Navigation'
import About from './pages/About'
import Home from './pages/Home'
import Topics from './pages/Topics'
import TopicDetails from './pages/TopicDetails'
import Profile from './pages/Profile'

export default class App extends React.Component {
  render () {
    return (
      <div>
        <Navigation></Navigation>
        <div className='container mainContainer' style={{ marginTop: 30 }}>
          <Switch>
            <Route exact path='/' component={Home}></Route>
            <Route exact path='/about' component={About}></Route>
            <Route exact path='/topics' component={Topics}></Route>
            <Route exact path='/topic/:id' component={TopicDetails}></Route>
            <Route exact path='/user/:username' component={Profile}></Route>
          </Switch>
        </div>
      </div>
    )
  }
}
