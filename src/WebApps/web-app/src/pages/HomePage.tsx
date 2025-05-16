import { ProductList } from 'components/products';
import React from 'react';

// Define interfaces for our data structures
interface Product {
  id: string;
  name: string;
  price: number;
  description: string;
  imageUrl: string;
}

interface FeaturedSection {
  title: string;
  description: string;
}

const featuredSections: FeaturedSection[] = [
  {
    title: 'Featured Products',
    description: 'Discover our latest and most popular items.',
  },
  {
    title: 'Special Offers',
    description: 'Check out our current deals and discounts.',
  },
  {
    title: 'New Arrivals',
    description: 'Explore our newest products.',
  },
];

const sampleProducts: Product[] = [
  {
    id: '1',
    name: 'Wireless Headphones',
    price: 99.99,
    description: 'High-quality wireless headphones with noise cancellation',
    imageUrl: 'https://placehold.co/300x300',
  },
  {
    id: '2',
    name: 'Smart Watch',
    price: 199.99,
    description: 'Feature-rich smartwatch with fitness tracking',
    imageUrl: 'https://placehold.co/300x300',
  },
  {
    id: '3',
    name: 'Laptop Backpack',
    price: 49.99,
    description: 'Durable laptop backpack with multiple compartments',
    imageUrl: 'https://placehold.co/300x300',
  },
  {
    id: '4',
    name: 'Wireless Mouse',
    price: 29.99,
    description: 'Ergonomic wireless mouse with precision tracking',
    imageUrl: 'https://placehold.co/300x300',
  },
  {
    id: '5',
    name: 'Mechanical Keyboard',
    price: 129.99,
    description: 'RGB mechanical keyboard with cherry mx switches',
    imageUrl: 'https://placehold.co/300x300',
  },
  {
    id: '6',
    name: 'USB-C Hub',
    price: 39.99,
    description: 'Multi-port USB-C hub with power delivery',
    imageUrl: 'https://placehold.co/300x300',
  },
  {
    id: '7',
    name: 'External SSD',
    price: 89.99,
    description: '500GB portable SSD with USB 3.1',
    imageUrl: 'https://placehold.co/300x300',
  },
  {
    id: '8',
    name: 'Webcam HD',
    price: 59.99,
    description: '1080p webcam with built-in microphone',
    imageUrl: 'https://placehold.co/300x300',
  },
  {
    id: '9',
    name: 'Gaming Mouse Pad',
    price: 19.99,
    description: 'Extended gaming mouse pad with stitched edges',
    imageUrl: 'https://placehold.co/300x300',
  }
];

const HomePage: React.FC = (): JSX.Element => {
  const renderFeaturedSection = ({ title, description }: FeaturedSection): JSX.Element => (
    <div key={title} className="bg-white p-6 rounded-lg shadow-md">
      <h2 className="text-xl font-semibold mb-4">{title}</h2>
      <p className="text-gray-600">{description}</p>
    </div>
  );

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-6">Welcome to EShop</h1>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {featuredSections.map(renderFeaturedSection)}
      </div>
      <div className="mt-8">
        <h2 className="text-2xl font-bold mb-6">Our Products</h2>
        <ProductList 
            products={sampleProducts}
            pagingInfo={{ currentPage: 1, totalPages: sampleProducts.length/3, pageSize: 3, totalItems: sampleProducts.length }}
            onPageChange={(page) => {
              // Handle page change
              console.log('Page changed to:', page);
            }}
          />
      </div>
    </div>
  );
};

export default HomePage;