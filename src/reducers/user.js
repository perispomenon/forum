import axios from 'axios'
import { REGISTER_USER } from '../constants/action-types'
import masterKey from '../config'

const initialState = {
  result: 0
}

const registerUser = async (state = initialState, action) => {
  console.log(masterKey)
  const rsp = await axios.post('users', { ...action.payload, access_token: masterKey })
  console.log(rsp)
}

export default registerUser
