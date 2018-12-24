import React from 'react'
import autoBind from 'react-autobind'
import { connect } from 'react-redux'
import PropTypes from 'prop-types'
import { Form, FormGroup, Label, Input, Button } from 'reactstrap'
import { registerUser } from '../actions/users'

const mapDispatchToProps = dispatch => ({
  registerUser: user => dispatch(registerUser(user))
})

const mapStateToProps = state => ({
  response: state.user.response
})

class SignUp extends React.Component {
  constructor () {
    super()
    autoBind(this)
    this.state = {
      username: '',
      email: '',
      password: '',
      confirmPassword: ''
    }
  }

  async handleSubmit (event) {
    event.preventDefault()
    // todo validata check
    await this.props.registerUser({
      name: this.state.username,
      email: this.state.email,
      password: this.state.password
    })
  }

  handleChange (event) {
    this.setState({ [event.target.name]: event.target.value })
  }

  render () {
    return (
      <Form style={{ paddingBottom: 40 }} onSubmit={this.handleSubmit}>
        <h1>Sign up</h1>
        <FormGroup>
          <Label>Username</Label>
          <Input type='text' name='username' value={this.state.username} placeholder='Enter username' onChange={this.handleChange} />
        </FormGroup>
        <FormGroup>
          <Label>Email</Label>
          <Input type='email' name='email' value={this.state.email} placeholder='Enter email' onChange={this.handleChange} />
        </FormGroup>
        <FormGroup>
          <Label>Password</Label>
          <Input type='password' name='password' value={this.state.password} placeholder='Enter password' onChange={this.handleChange} />
        </FormGroup>
        <FormGroup>
          <Label>Confirm password</Label>
          <Input type='password' name='confirmPassword' value={this.state.confirmPassword} placeholder='Confirm password' onChange={this.handleChange} />
        </FormGroup>
        <Button color='secondary' type='submit'>Sign up</Button>
      </Form>
    )
  }
}

SignUp.propTypes = {
  registerUser: PropTypes.func,
  response: PropTypes.object
}

export default connect(mapStateToProps, mapDispatchToProps)(SignUp)
