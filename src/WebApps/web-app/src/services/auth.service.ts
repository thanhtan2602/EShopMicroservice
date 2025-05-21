import Cookies from 'js-cookie'

export const saveAccessTokenToCookie = (accessToken: string) => {
  Cookies.set('accessToken', accessToken, { secure: true })
}

export const saveTokensToCookie = (accessToken: string, refreshToken: string) => {
  Cookies.set('accessToken', accessToken, { secure: true })
  Cookies.set('refreshToken', refreshToken, { secure: true })
}

export const removeTokens = () => {
  Cookies.remove('accessToken')
  Cookies.remove('refreshToken')
}

export const getAccessToken = () => Cookies.get('accessToken')
export const getRefreshToken = () => Cookies.get('refreshToken')


