export const AUTH_BASE_URL =
  process.env.NODE_ENV === 'production'
    ? 'https://localhost:6066/auth-service'
    : 'https://localhost:5056/auth-service'
