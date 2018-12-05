import React from 'react'
import autoBind from 'react-autobind'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { Form, FormGroup, Label, Input, Button } from 'reactstrap'
import { registerUser } from '../actions/'

const mapDispatchToProps = dispatch => {
  return {
    registerUser: user => dispatch(registerUser(user))
  }
}

class Login extends React.Component {
  constructor () {
    super()
    autoBind(this)
  }

  handleSubmit () {
    this.props.registerUser({
      name: 123
    })
  }

  render () {
    return (
      <Form style={{ paddingBottom: 40 }} onSubmit={this.handleSubmit}>
        <h1>Login</h1>
        <FormGroup>
          <Label>Username</Label>
          <Input type='text' name='username' placeholder='Enter username' />
        </FormGroup>
        <FormGroup>
          <Label>Password</Label>
          <Input type='password' name='password' placeholder='Enter password' />
        </FormGroup>
        <Button color='secondary' type='submit'>Login</Button>
      </Form>
    )
  }
}

Login.propTypes = {
  registerUser: PropTypes.func
}

export default connect(null, mapDispatchToProps)(Login)
