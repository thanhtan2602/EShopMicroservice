export interface User {
  id: number;
  name: string;
  email: string;
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
