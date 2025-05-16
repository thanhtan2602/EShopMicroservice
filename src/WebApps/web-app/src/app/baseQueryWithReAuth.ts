import { fetchBaseQuery, BaseQueryFn } from '@reduxjs/toolkit/query/react'
import type { FetchArgs, FetchBaseQueryError } from '@reduxjs/toolkit/query'
import { logout, setUser } from '../features/auth/authSlice'
import { saveAccessTokenToCookie, getRefreshTokenFromCookie } from '../services/auth.service'
import { RootState } from './store'

const baseUrl = process.env.NODE_ENV === 'production'
  ? 'https://localhost:6066/auth-service'
  : 'https://localhost:5056/auth-service'

// Cấu hình baseQuery gốc
const baseQuery = fetchBaseQuery({
  baseUrl,
  credentials: 'include', // để gửi cookie (access token)
  prepareHeaders: (headers, { getState }) => {
    const state = getState() as RootState
    const token = state.auth.accessToken
    if (token) {
      headers.set('Authorization', `Bearer ${token}`)
    }
    return headers
  },
})

export const baseQueryWithReAuth: BaseQueryFn<string | FetchArgs, unknown, FetchBaseQueryError> = async (
  args,
  api,
  extraOptions
) => {
  let result = await baseQuery(args, api, extraOptions)

  if (result.error && result.error.status === 401) {
    try {
      const refreshToken = getRefreshTokenFromCookie()

      if (!refreshToken) {
        api.dispatch(logout())
        return result
      }

      const refreshResult = await baseQuery(
        {
          url: '/refresh-token',
          method: 'POST',
          body: { refreshToken },
        },
        api,
        extraOptions
      )

      if (refreshResult.data) {
        const newAccessToken = (refreshResult.data as any).accessToken
        const user = (refreshResult.data as any).user

        saveAccessTokenToCookie(newAccessToken) 

        api.dispatch(setUser(user))

        result = await baseQuery(args, api, extraOptions)
      } else {
        api.dispatch(logout())
      }
    } catch (error) {
      console.error('Refresh token failed', error)
      api.dispatch(logout())
    }
  }

  return result
}
