import { Action, createReducer, on } from '@ngrx/store';
import * as authActions from './actions';
import { initialState, State } from './state';

const reducer = createReducer(
  initialState,

  // Login
  on(authActions.login, (state) => {
    return {
      ...state,
      isLoading: true,
      error: null,
    };
  }),
  on(authActions.loginSuccess, (state, { user }) => {
    localStorage.setItem('token', user.token);

    return {
      ...state,
      token: user.token,
      user: user.user,
      isisLoading: false,
      error: null,
    };
  }),
  on(authActions.loginFailure, (state, { error }) => {
    return {
      ...state,
      token: null,
      user: null,
      isLoading: false,
      error,
    };
  }),

  // Register
  on(authActions.register, (state) => {
    return {
      ...state,
      isLoading: true,
      error: null,
    };
  }),
  on(authActions.registerSuccess, (state) => {
    return {
      ...state,
      isLoading: false,
      error: null,
    };
  }),
  on(authActions.loginFailure, (state, { error }) => {
    return {
      ...state,
      isLoading: false,
      error,
    };
  }),

  // Logout
  on(authActions.logout, (state) => {
    localStorage.removeItem('token');

    return {
      ...state,
      token: null,
      user: null,
    };
  }),

  // Check if email is taken
  on(authActions.checkEmailTaken, (state) => {
    return {
      ...state,
      emailTaken: null,
      isLoading: true,
      error: null,
    };
  }),
  on(authActions.checkEmailTakenSuccess, (state, { result }) => {
    return {
      ...state,
      emailTaken: result,
      isLoading: false,
      error: null,
    };
  }),
  on(authActions.checkEmailTakenFailure, (state, { error }) => {
    return {
      ...state,
      emailTaken: null,
      isLoading: false,
      error,
    };
  })
);

export function authReducer(state: State | undefined, action: Action) {
  return reducer(state, action);
}
