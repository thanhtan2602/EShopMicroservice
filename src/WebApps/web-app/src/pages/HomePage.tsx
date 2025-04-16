import React from 'react';

const HomePage: React.FC = () => {
  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6">Welcome to EShop</h1>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {/* Featured Products */}
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-4">Featured Products</h2>
          <p className="text-gray-600">Discover our latest and most popular items.</p>
        </div>

        {/* Special Offers */}
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-4">Special Offers</h2>
          <p className="text-gray-600">Check out our current deals and discounts.</p>
        </div>

        {/* New Arrivals */}
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h2 className="text-xl font-semibold mb-4">New Arrivals</h2>
          <p className="text-gray-600">Explore our newest products.</p>
        </div>
      </div>
    </div>
  );
};

export default HomePage; 