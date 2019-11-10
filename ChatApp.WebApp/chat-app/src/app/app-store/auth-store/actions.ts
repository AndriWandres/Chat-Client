import { createAction, props, union } from '@ngrx/store';
import { AuthenticatedUser, LoginDto } from 'src/models/auth/login';
import { RegisterDto } from 'src/models/auth/register';

// Type constants
export enum ActionTypes {
  LOGIN = '[Auth] Login',
  LOGIN_SUCCESS = '[Auth] Login Success',
  LOGIN_FAILURE = '[Auth] Login Failure',

  REGISTER = '[Auth] Register',
  REGISTER_SUCCESS = '[Auth] Register Success',
  REGISTER_FAILURE = '[Auth] Register Failure',

  LOGOUT = '[Auth] Logout',

  CHECK_EMAIL_TAKEN = '[Auth] Check if email taken',
  CHECK_EMAIL_TAKEN_SUCCESS = '[Auth] Check if email taken Success',
  CHECK_EMAIL_TAKEN_FAILURE = '[Auth] Check if email taken Failure',
}

// Login
export const login = createAction(ActionTypes.LOGIN, props<{ credentials: LoginDto }>());
export const loginSuccess = createAction(ActionTypes.LOGIN_SUCCESS, props<{ user: AuthenticatedUser }>());
export const loginFailure = createAction(ActionTypes.LOGIN_FAILURE, props<{ error: any }>());

// Register
export const register = createAction(ActionTypes.REGISTER, props<{ credentials: RegisterDto }>());
export const registerSuccess = createAction(ActionTypes.REGISTER_SUCCESS);
export const registerFailure = createAction(ActionTypes.REGISTER_FAILURE, props<{ error: any }>());

// Logout
export const logout = createAction(ActionTypes.LOGOUT);

// Check if email is taken
export const checkEmailTaken = createAction(ActionTypes.CHECK_EMAIL_TAKEN, props<{ email: string }>());
export const checkEmailTakenSuccess = createAction(ActionTypes.CHECK_EMAIL_TAKEN_SUCCESS, props<{ result: boolean }>());
export const checkEmailTakenFailure = createAction(ActionTypes.CHECK_EMAIL_TAKEN_FAILURE, props<{ error: any }>());

// Union type
const allActions = union({
  login,
  loginSuccess,
  loginFailure,

  register,
  registerSuccess,
  registerFailure,

  logout,

  checkEmailTaken,
  checkEmailTakenSuccess,
  checkEmailTakenFailure
});

export type AuthActionUnion = typeof allActions;
