import { logout } from 'features/auth/authSlice'
import { FaUser } from 'react-icons/fa'
import { useDispatch } from 'react-redux'
import { Link, useNavigate } from 'react-router-dom'
import { removeTokens } from 'services/auth.service'
import { User } from 'types/auth'

interface Props {
  user: User | null
}

const HeaderUserProfile = (props: Props) => {
  const dispatch = useDispatch()
  const navigate = useNavigate()
  
  const { user } = props
  console.log('user', user)
  
  const handleLogout = () => {
    removeTokens()

    dispatch(logout())

    navigate('/login')
  }

  return (
    <>
      {user ? (
        <div className="relative group">
          <div className="flex items-center gap-2 cursor-pointer">
            <img
              width={32}
              height={32}
              src={user.image || '/default-avatar.png'}
              alt="Avatar"
              className="rounded-full"
            />
            <span className="text-sm font-medium">{user.fullName || 'no-name'}</span>
          </div>
          <div className="absolute left-0 hidden group-hover:block w-48 bg-white shadow-lg py-2 z-10 rounded-md">
            <Link to="/profile" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">
              Profile
            </Link>
            <button
              onClick={() => {handleLogout()}}
              className="block w-full text-left px-4 py-2 text-gray-800 hover:bg-gray-100"
            >
              Logout
            </button>
          </div>
        </div>
      ) : (
        <button
          onClick={() => window.location.href = '/login'}
          className="text-gray-600 hover:text-primary-600"
        >
          <FaUser className="text-2xl" />
        </button>
      )}
    </>
  )
}

export default HeaderUserProfile
