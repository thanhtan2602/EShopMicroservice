import { Link } from 'react-router-dom';
import HeaderUserProfile from './HeaderUserProfile';
import HeaderCard from './HeaderCard';
import HeaderSearchBox from './HeaderSearchBox';
import HeaderMenu from './HeaderMenu';
import { useAppSelector } from 'app/hooks'

const Header = () => {

  const user = useAppSelector((state) => state.auth.user)

  return (
    <header className="bg-white shadow-md">
      <div className="container mx-auto px-4">
        <div className="flex items-center justify-between h-16">
          <Link to="/" className="text-2xl font-bold text-primary-600">
            EShop
          </Link>
          <HeaderSearchBox />
          <div className="flex items-center space-x-6">
            <HeaderCard />
            <HeaderUserProfile user={user} />
          </div>
        </div>
        <HeaderMenu />
      </div>
    </header>
  );
};

export default Header; 