export interface User {
  id: number;
  userName: string;
  email: string;
  password: string;
  status: string;
  isAdmin: boolean;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  user: User | null;
  token: string | null;
}

export interface SignUpRequest {
  name: string;
  email: string;
  password: string;
  country: number;
}

export interface SignUpResponse {
  maessage: string;
  issuccess: boolean;
}

export interface AuthState {
  user: User | null;
  token: string | null;
}
