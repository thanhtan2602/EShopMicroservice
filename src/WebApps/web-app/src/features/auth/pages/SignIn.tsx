// SignIn.tsx
import React, { useEffect, useState, useCallback } from 'react'
import { useLoginMutation } from '../authApi'
import { useAppDispatch } from '../../../app/hooks'
import { Link, useNavigate } from 'react-router-dom'
import { setUser } from '../authSlice'
import { saveTokensToCookie } from '../../../services/auth.service'
import { User } from 'types/auth'

const SignIn: React.FC = () => {
  const [formData, setFormData] = useState({ email: '', password: '' })
  const [login, { isLoading }] = useLoginMutation()
  const dispatch = useAppDispatch()
  const navigate = useNavigate()
  const [error, setError] = useState<string | null>(null)

  // Hàm cập nhật thông tin đăng nhập sau khi nhận được token
  const updateUserLogin = useCallback(
    (user: User, accessToken: string, refreshToken: string) => {
      saveTokensToCookie(accessToken, refreshToken)
      dispatch(setUser(user))
    },
    [dispatch]
  )

  // Custom hook nội bộ: lắng nghe postMessage từ popup Google login
  useEffect(() => {
    const messageHandler = (event: MessageEvent) => {
      // Kiểm tra origin (đảm bảo an toàn)
      if (event.origin !== 'https://localhost:5056') return

      const { accessToken, refreshToken, user } = event.data
      if (accessToken && refreshToken && user) {
        updateUserLogin(user, accessToken, refreshToken)
        navigate('/')
      }
    }
    window.addEventListener('message', messageHandler)
    return () => window.removeEventListener('message', messageHandler)
  }, [navigate, updateUserLogin])

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({ ...prev, [e.target.name]: e.target.value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setError(null)
    try {
      const result = await login(formData).unwrap()
      updateUserLogin(result.user, result.accessToken, result.refreshToken)
      navigate('/')
    } catch (err: any) {
      console.error('Login failed:', err)
      setError(err?.data?.message || 'Đăng nhập thất bại')
    }
  }

  const openGoogleLogin = () => {
    const width = 500
    const height = 600
    const left = window.screenX + (window.outerWidth - width) / 2
    const top = window.screenY + (window.outerHeight - height) / 2
    window.open(
      "https://localhost:5056/auth-service/google/signin",
      "_blank",
      `width=${width},height=${height},left=${left},top=${top}`
    )
  }

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-100">
      <div className="w-96 rounded-lg bg-white p-8 shadow-md">
        <h2 className="mb-6 text-center text-2xl font-bold text-gray-900">Sign In</h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block text-sm font-medium text-gray-700">Email</label>
            <input
              type="text"
              name="email"
              className="mt-1 w-full rounded-md border border-gray-300 p-2 focus:border-blue-500 focus:outline-none"
              placeholder="Enter your email"
              value={formData.email}
              onChange={handleChange}
              autoComplete="off"
            />
          </div>
          <div className="mb-6">
            <label className="block text-sm font-medium text-gray-700">Password</label>
            <input
              type="password"
              name="password"
              className="mt-1 w-full rounded-md border border-gray-300 p-2 focus:border-blue-500 focus:outline-none"
              placeholder="Enter your password"
              value={formData.password}
              onChange={handleChange}
            />
          </div>
          <button
            type="submit"
            disabled={isLoading}
            className="mb-4 w-full rounded-md bg-blue-600 py-2 px-4 text-white hover:bg-blue-700"
          >
            {isLoading ? 'Logging in...' : 'Login'}
          </button>
          {error && <div className="text-red-600">{error}</div>}
        </form>
        <div className="relative mb-4">
          <div className="absolute inset-0 flex items-center">
            <div className="w-full border-t border-gray-300"></div>
          </div>
          <div className="relative flex justify-center text-sm">
            <span className="bg-white px-2 text-gray-500">Or continue with</span>
          </div>
        </div>
        <button
          onClick={openGoogleLogin}
          type="button"
          className="mb-4 flex w-full items-center justify-center rounded-md border border-gray-300 bg-white py-2 px-4 text-gray-700 hover:bg-gray-50"
        >
          <svg className="mr-2 h-5 w-5" viewBox="0 0 24 24">
            <path
              d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"
              fill="#4285F4"
            />
            <path
              d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"
              fill="#34A853"
            />
            <path
              d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z"
              fill="#FBBC05"
            />
            <path
              d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z"
              fill="#EA4335"
            />
          </svg>
          Sign in with Google
        </button>
        <div className="text-center text-sm text-gray-600">
          Don't have an account?{' '}
          <Link to="/signup" className="font-medium text-blue-600 hover:text-blue-500">
            Sign up
          </Link>
        </div>
      </div>
    </div>
  )
}

export default SignIn
