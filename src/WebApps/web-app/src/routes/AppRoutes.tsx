import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { Cart, HomePage } from '../pages';
import { Account, GoogleCallback, SignIn, SignUp } from 'features/auth/pages';

// Import your page components here
const Products = () => <div className="container mx-auto px-4 py-8">Products Page</div>;
const Categories = () => <div className="container mx-auto px-4 py-8">Categories Page</div>;
const Deals = () => <div className="container mx-auto px-4 py-8">Deals Page</div>;
const About = () => <div className="container mx-auto px-4 py-8">About Page</div>;
const FAQ = () => <div className="container mx-auto px-4 py-8">FAQ Page</div>;
const Shipping = () => <div className="container mx-auto px-4 py-8">Shipping Policy Page</div>;
const Returns = () => <div className="container mx-auto px-4 py-8">Returns & Refunds Page</div>;
const Privacy = () => <div className="container mx-auto px-4 py-8">Privacy Policy Page</div>;
const NotFound = () => <div className="container mx-auto px-4 py-8 text-center">404 - Page Not Found</div>;

const AppRoutes: React.FC = () => {
  return (
    <Routes>
      {/* Main Routes */}
      <Route path="/" element={<HomePage />} />
      <Route path="/products" element={<Products />} />
      <Route path="/categories" element={<Categories />} />
      <Route path="/deals" element={<Deals />} />
      <Route path="/about" element={<About />} />

      {/* User Routes */}
      <Route path="/cart" element={<Cart />} />
      <Route path="/account" element={<Account />} />
      <Route path="/login" element={<SignIn />} />
      <Route path="/signup" element={<SignUp />} />
      <Route path="/auth/google/callback" element={<GoogleCallback />} />

      {/* Policy Routes */}
      <Route path="/faq" element={<FAQ />} />
      <Route path="/shipping" element={<Shipping />} />
      <Route path="/returns" element={<Returns />} />
      <Route path="/privacy" element={<Privacy />} />

      {/* 404 Route */}
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};

export default AppRoutes; 