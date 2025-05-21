export interface User {
  id: string
  email: string
  fullName: string
  phoneNumber: string
  image: string
}

export interface LoginRequest {
  email: string
  password: string
}

export interface LoginResponse {
  accessToken: string
  refreshToken: string
  user: User
}

export interface RegisterRequest {
  email: string
  password: string
  firstName: string
  lastName: string
}

export interface RegisterResponse {
  isSuccess: boolean
}

export interface UserProfileResponse {
  user: User;
}