import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithReAuth } from '../../app/baseQueryWithReAuth'
import { saveTokensToCookie } from '../../services/auth.service'
import { setUser } from '../../features/auth/authSlice'
import { User, LoginRequest, LoginResponse, RegisterRequest, RegisterResponse } from '../../types/auth'

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
      async onQueryStarted(arg, { dispatch, queryFulfilled }) {
        try {
          const { data } = await queryFulfilled

          saveTokensToCookie(data.accessToken, data.refreshToken)
          dispatch(setUser(data.user))
        } catch (err) {
          console.error('Login failed:', err)
        }
      },
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

    getCurrentUser: builder.query<User, void>({
      query: () => '/me',
    }),
  }),
})

export const { useLoginMutation, useRegisterMutation, useRefreshTokenMutation, useGetCurrentUserQuery } = authApi
