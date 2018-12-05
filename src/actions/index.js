import { REGISTER_USER } from '../constants/action-types'

export const registerUser = user => ({ type: REGISTER_USER, payload: user })
