import User from './User';

export default interface AuthResponse {
  user: User;
  token: string;
}