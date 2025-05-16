import React from "react";

const Cart: React.FC = () => {
  const cartItems = [
    { id: 1, name: "Product 1", price: 29.99, quantity: 2 },
    { id: 2, name: "Product 2", price: 49.99, quantity: 1 },
  ];

  const calculateTotal = () =>
    cartItems.reduce((total, item) => total + item.price * item.quantity, 0);

  const deliveryInfo = {
    address: "123 Main Street, Springfield",
    estimatedDelivery: "3-5 business days",
  };

  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-lg p-8">
        <h1 className="text-3xl font-bold mb-6 text-gray-800">Shopping Cart</h1>
        {cartItems.length > 0 ? (
          <div>
            <ul className="divide-y divide-gray-200">
              {cartItems.map((item) => (
                <li
                  key={item.id}
                  className="flex justify-between items-center py-4 hover:bg-gray-100 px-4 rounded-lg transition"
                >
                  <div>
                    <h2 className="text-lg font-semibold text-gray-700">
                      {item.name}
                    </h2>
                    <p className="text-sm text-gray-500">
                      Quantity: {item.quantity}
                    </p>
                  </div>
                  <p className="text-lg font-semibold text-gray-800">
                    ${item.price.toFixed(2)}
                  </p>
                </li>
              ))}
            </ul>
            <div className="mt-8 flex justify-between items-center border-t pt-4">
              <h2 className="text-xl font-bold text-gray-800">Total:</h2>
              <p className="text-xl font-bold text-blue-600">
                ${calculateTotal().toFixed(2)}
              </p>
            </div>
            <div className="mt-8 bg-blue-50 p-6 rounded-lg">
              <h2 className="text-lg font-bold mb-2 text-blue-700">
                Delivery Information
              </h2>
              <p className="text-gray-600">
                <strong>Address:</strong> {deliveryInfo.address}
              </p>
              <p className="text-gray-600">
                <strong>Estimated Delivery:</strong>{" "}
                {deliveryInfo.estimatedDelivery}
              </p>
            </div>
            <button className="mt-8 w-full bg-blue-600 text-white py-3 px-6 rounded-lg font-semibold text-lg hover:bg-blue-700 transition">
              Proceed to Checkout
            </button>
          </div>
        ) : (
          <p className="text-gray-600 text-center text-lg">
            Your cart is empty.
          </p>
        )}
      </div>
    </div>
  );
};

export default Cart;