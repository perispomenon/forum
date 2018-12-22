import * as actionTypes from '../constants/action-types'

const initialState = {
  response: null
}

export default (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.REGISTER_USER_SUCCESS:
      return { ...state, err: action.response }
    case actionTypes.REGISTER_USER_FAILURE:
      return { ...state, err: action.response }
    default:
      return state
  }
}
