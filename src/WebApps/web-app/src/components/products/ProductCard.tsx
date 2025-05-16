const ProductCard = () => {
  return (
    <div className="bg-white shadow-md rounded-lg p-4">
      <img
        src="https://via.placeholder.com/150"
        alt="Product"
        className="w-full h-48 object-cover rounded-t-lg"
      />
      <h2 className="text-xl font-bold mt-2">Product Name</h2>
      <p className="text-gray-700 mt-1">$19.99</p>
      <button className="mt-4 bg-blue-500 text-white py-2 px-4 rounded">
        Add to Cart
      </button>
    </div>
  );
}

export default ProductCard;