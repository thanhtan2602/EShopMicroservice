import Cookies from 'js-cookie'

export const saveAccessTokenToCookie = (accessToken: string) => {
  Cookies.set('accessToken', accessToken, { secure: true })
}

export const saveTokensToCookie = (accessToken: string, refreshToken: string) => {
  Cookies.set('accessToken', accessToken, { secure: true })
  Cookies.set('refreshToken', refreshToken, { secure: true })
}

export const getRefreshTokenFromCookie = (): string | undefined => {
  return Cookies.get('refreshToken')
}

export const clearAuthCookies = () => {
  Cookies.remove('accessToken')
  Cookies.remove('refreshToken')
}
