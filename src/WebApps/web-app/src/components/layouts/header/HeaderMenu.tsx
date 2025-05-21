import React from 'react'
import { Link } from 'react-router-dom'

export default function HeaderMenu() {
  return (
    <>
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
    </>
  )
}
