import React from 'react'
// import autoBind from 'react-autobind'

import { Form, FormGroup, Label, Input, Button } from 'reactstrap'

class SignUp extends React.Component {
  render () {
    return (
      <Form style={{ paddingBottom: 40 }}>
        <h1>Sign up</h1>
        <FormGroup>
          <Label>Username</Label>
          <Input type='text' name='username' placeholder='Enter username' />
        </FormGroup>
        <FormGroup>
          <Label>Email</Label>
          <Input type='email' name='email' placeholder='Enter email' />
        </FormGroup>
        <FormGroup>
          <Label>Password</Label>
          <Input type='password' name='password' placeholder='Enter password' />
        </FormGroup>
        <FormGroup>
          <Label>Confirm password</Label>
          <Input type='password' name='password' placeholder='Confirm password' />
        </FormGroup>
        <Button color='secondary' type='submit'>Sign up</Button>
      </Form>
    )
  }
}

export default SignUp
