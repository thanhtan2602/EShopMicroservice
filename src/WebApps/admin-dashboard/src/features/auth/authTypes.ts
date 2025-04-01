export interface UserModel {
  id: number;
  userName: string;
  email: string;
  status: string;
  isAdmin: boolean;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  user: UserModel | null;
  accessToken: string | null;
  refreshToken: string | null;
}

export interface SignUpRequest {
  fullName: string;
  password: string;
  email: string;
}

export interface SignUpResponse {
  isSuccess: boolean;
}

export interface RefreshTokenResponse {
  newAccessToken: string | null;
  newRefreshToken: string | null;
}
