import { AppUser } from './user';

export interface LoginDto {
  email: string;
  password: string;
}

export interface AuthenticatedUser {
  token: string;
  user: AppUser;
}
