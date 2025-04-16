import React from 'react';
import { Link } from 'react-router-dom';
import { FaFacebookF, FaTwitter, FaInstagram, FaLinkedinIn } from 'react-icons/fa';

const Footer = () => {
  return (
    <footer className="bg-secondary-800 text-white">
      <div className="container mx-auto px-4 py-12">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          {/* Company Info */}
          <div>
            <h3 className="text-xl font-bold mb-4">EShop</h3>
            <p className="text-secondary-300">
              Your one-stop shop for all your shopping needs. Quality products at competitive prices.
            </p>
          </div>

          {/* Quick Links */}
          <div>
            <h3 className="text-xl font-bold mb-4">Quick Links</h3>
            <ul className="space-y-2">
              <li>
                <Link to="/" className="text-secondary-300 hover:text-white">
                  Home
                </Link>
              </li>
              <li>
                <Link to="/products" className="text-secondary-300 hover:text-white">
                  Products
                </Link>
              </li>
              <li>
                <Link to="/categories" className="text-secondary-300 hover:text-white">
                  Categories
                </Link>
              </li>
              <li>
                <Link to="/deals" className="text-secondary-300 hover:text-white">
                  Deals
                </Link>
              </li>
            </ul>
          </div>

          {/* Customer Service */}
          <div>
            <h3 className="text-xl font-bold mb-4">Customer Service</h3>
            <ul className="space-y-2">
              <li>
                <Link to="/faq" className="text-secondary-300 hover:text-white">
                  FAQ
                </Link>
              </li>
              <li>
                <Link to="/shipping" className="text-secondary-300 hover:text-white">
                  Shipping Policy
                </Link>
              </li>
              <li>
                <Link to="/returns" className="text-secondary-300 hover:text-white">
                  Returns & Refunds
                </Link>
              </li>
              <li>
                <Link to="/privacy" className="text-secondary-300 hover:text-white">
                  Privacy Policy
                </Link>
              </li>
            </ul>
          </div>

          {/* Social Media */}
          <div>
            <h3 className="text-xl font-bold mb-4">Follow Us</h3>
            <div className="flex space-x-4">
              <a href="#" className="text-secondary-300 hover:text-white">
                {React.createElement(FaFacebookF, { className: "text-2xl" })}
              </a>
              <a href="#" className="text-secondary-300 hover:text-white">
                {React.createElement(FaTwitter, { className: "text-2xl" })}
              </a>
              <a href="#" className="text-secondary-300 hover:text-white">
                {React.createElement(FaInstagram, { className: "text-2xl" })}
              </a>
              <a href="#" className="text-secondary-300 hover:text-white">
                {React.createElement(FaLinkedinIn, { className: "text-2xl" })}
              </a>
            </div>
          </div>
        </div>

        {/* Copyright */}
        <div className="border-t border-secondary-700 mt-8 pt-8 text-center text-secondary-300">
          <p>&copy; {new Date().getFullYear()} EShop. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer; 