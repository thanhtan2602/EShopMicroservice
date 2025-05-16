import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { FaSearch, FaShoppingCart, FaUser } from 'react-icons/fa';
import { useAppSelector } from 'app/hooks';

const Header = () => {
  const [searchQuery, setSearchQuery] = useState('');

  const user = useAppSelector((state) => state.auth.user)

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Searching for:', searchQuery);
  };

  return (
    <header className="bg-white shadow-md">
      <div className="container mx-auto px-4">
        <div className="flex items-center justify-between h-16">
          <Link to="/" className="text-2xl font-bold text-primary-600">
            EShop
          </Link>

          <form onSubmit={handleSearch} className="flex-1 max-w-2xl mx-4">
            <div className="relative">
              <input
                type="text"
                placeholder="Search products..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-primary-500"
              />
              <button
                type="submit"
                className="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-primary-600"
              >
                <FaSearch />
              </button>
            </div>
          </form>

          <div className="flex items-center space-x-6">
            <Link to="/cart" className="relative text-gray-600 hover:text-primary-600">
              <FaShoppingCart className="text-2xl" />
              <span className="absolute -top-2 -right-2 bg-primary-600 text-white text-xs rounded-full h-5 w-5 flex items-center justify-center">
                0
              </span>
            </Link>

            {user ? (
              <div className="flex items-center gap-2">
                <img
                  src={user.image || '/default-avatar.png'}
                  alt="Avatar"
                  className="h-8 w-8 rounded-full"
                />
                <span className="text-sm font-medium">{user.fullName || 'no-name'}</span>
              </div>
            ) : (
              <button
                onClick={() => window.location.href = '/login'}
                className="text-gray-600 hover:text-primary-600"
              >
                <FaUser className="text-2xl" />
              </button>
            )}
          </div>
        </div>
        <nav className="py-2 border-t">
          <ul className="flex space-x-6">
            <li className="group relative">
              <button className="flex items-center space-x-1 text-gray-700 hover:text-primary-600">
                <span>Electronics</span>
                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                </svg>
              </button>
              <div className="absolute hidden group-hover:block w-48 bg-white shadow-lg py-2 z-10">
                <Link to="/category/phones" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">Phones</Link>
                <Link to="/category/laptops" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">Laptops</Link>
                <Link to="/category/accessories" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">Accessories</Link>
              </div>
            </li>
            <li className="group relative">
              <button className="flex items-center space-x-1 text-gray-700 hover:text-primary-600">
                <span>Fashion</span>
                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                </svg>
              </button>
              <div className="absolute hidden group-hover:block w-48 bg-white shadow-lg py-2 z-10">
                <Link to="/category/mens" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">Men's Wear</Link>
                <Link to="/category/womens" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">Women's Wear</Link>
                <Link to="/category/kids" className="block px-4 py-2 text-gray-800 hover:bg-gray-100">Kids</Link>
              </div>
            </li>
            <li>
              <Link to="/category/home" className="text-gray-700 hover:text-primary-600">Home & Living</Link>
            </li>
            <li>
              <Link to="/category/books" className="text-gray-700 hover:text-primary-600">Books</Link>
            </li>
          </ul>
        </nav>
      </div>
    </header>
  );
};

export default Header; 