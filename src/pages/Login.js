import React from 'react'
import autoBind from 'react-autobind'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Form, FormGroup, Label, Input, Button } from 'reactstrap'
import { registerUser } from '../actions/users'

const mapDispatchToProps = dispatch => ({
  registerUser: user => dispatch(registerUser(user))
})

const mapStateToProps = state => ({
  result: state
})

class Login extends React.Component {
  constructor () {
    super()
    autoBind(this)
    this.state = {
      username: '',
      password: ''
    }
  }

  async handleSubmit (event) {
    event.preventDefault()
    await this.props.registerUser({
      name: this.state.username,
      password: this.state.password
    })
  }

  handleChange (event) {
    this.setState({ [event.target.name]: event.target.value })
  }

  render () {
    return (
      <Form style={{ paddingBottom: 40 }} onSubmit={this.handleSubmit}>
        <h1>Login</h1>
        <FormGroup>
          <Label>Username</Label>
          <Input type='text' name='username' value={this.state.username} placeholder='Enter username' onChange={this.handleChange} />
        </FormGroup>
        <FormGroup>
          <Label>Password</Label>
          <Input type='password' name='password' value={this.state.password} placeholder='Enter password' onChange={this.handleChange}/>
        </FormGroup>
        <Button color='secondary' type='submit'>Login</Button>
      </Form>
    )
  }
}

Login.propTypes = {
  registerUser: PropTypes.func
}

export default connect(mapStateToProps, mapDispatchToProps)(Login)
