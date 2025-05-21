import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithReAuth } from '../../app/baseQueryWithReAuth'
import { User, LoginRequest, LoginResponse, RegisterRequest, RegisterResponse, UserProfileResponse } from '../../types/auth'

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: baseQueryWithReAuth,
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginRequest>({
      query: (credentials) => ({
        url: '/login',
        method: 'POST',
        body: credentials,
      }),
    }),

    register: builder.mutation<RegisterResponse, RegisterRequest>({
      query: (newUser) => ({
        url: '/register',
        method: 'POST',
        body: newUser,
      }),
    }),

    refreshToken: builder.mutation<{ accessToken: string; user: User }, { refreshToken: string }>({
      query: (body) => ({
        url: '/refresh-token',
        method: 'POST',
        body,
      }),
    }),

    getUserProfile: builder.query<UserProfileResponse, void>({
      query: () => '/me',
    }),
  }),
})

export const { useLoginMutation, useRegisterMutation, useRefreshTokenMutation, useGetUserProfileQuery } = authApi
