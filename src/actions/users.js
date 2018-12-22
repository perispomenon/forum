import axios from 'axios'
import * as actionTypes from '../constants/action-types'
import config from '../config'

// Register user api call
export const registerUserSuccess = response => ({ type: actionTypes.REGISTER_USER_SUCCESS, response })
export const registerUserFailure = response => ({ type: actionTypes.REGISTER_USER_FAILURE, response })

export const registerUser = user => async dispatch => {
  const response = await axios.post('users', { ...user, access_token: config.masterKey })
  console.log(response)
  if (response.status !== 200 && response.status !== 201) {
    return dispatch(registerUserFailure(response))
  }
  return dispatch(registerUserSuccess(response))
}

// Login user api call
