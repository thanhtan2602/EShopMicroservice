import React from 'react'
import { FaShoppingCart } from 'react-icons/fa'
import { Link } from 'react-router-dom'

export default function HeaderCard() {
  return (
    <>
      <Link to="/cart" className="relative text-gray-600 hover:text-primary-600">
        <FaShoppingCart className="text-2xl" />
        <span className="absolute -top-2 -right-2 bg-primary-600 text-white text-xs rounded-full h-5 w-5 flex items-center justify-center">
          0
        </span>
      </Link>
    </>
  )
}
