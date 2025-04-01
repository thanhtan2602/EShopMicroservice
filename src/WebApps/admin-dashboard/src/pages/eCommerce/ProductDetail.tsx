import React from "react";
import { useParams } from "react-router-dom";
import { useGetProductByIdQuery } from "../../features/product/productApi";

export default function ProductDetail() {
  const { productId } = useParams();
  const { data: product, error, isLoading } = useGetProductByIdQuery(productId ?? "");
  if (isLoading) {
    return <div>Loading product details...</div>;
  }

  if (error) {
    return <div>Error loading product details.</div>;
  }

  if (!product) {
    return <div>Product not found.</div>;
  }

  return (
    <>
      {product && (
        <div className="max-w-4xl mx-auto bg-white rounded-xl shadow-lg overflow-hidden mt-20">
          <div className="md:flex">
            <div className="md:w-1/3 bg-gray-100 p-8">
              {/* Placeholder for product image */}
              <div className="w-full h-64 bg-gray-200 rounded-lg flex items-center justify-center">
                <svg className="w-24 h-24 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
                </svg>
              </div>
            </div>
            
            <div className="p-8 md:w-2/3">
              <div className="uppercase tracking-wide text-sm text-indigo-500 font-semibold">
                Product ID: {product?.product?.id}
              </div>
              <h1 className="mt-2 text-3xl font-bold text-gray-900">
                {product?.product.name}
              </h1>
              
              <div className="mt-4">
                <div className="text-2xl font-bold text-gray-900">
                  ${product?.product.price.toFixed(2)}
                </div>
                <div className="mt-2 inline-block px-3 py-1 bg-green-100 text-green-800 rounded-full text-sm">
                  {product?.product.category}
                </div>
              </div>

              <div className="mt-6">
                <h2 className="text-gray-500 text-sm uppercase tracking-wide font-semibold">Description</h2>
                <p className="mt-2 text-gray-600 leading-relaxed">
                  {product?.product.description}
                </p>
              </div>

              <div className="mt-8 flex space-x-4">
                <button className="bg-indigo-600 text-white px-6 py-2 rounded-lg hover:bg-indigo-700 transition-colors">
                  Edit Product
                </button>
                <button className="border border-gray-300 text-gray-700 px-6 py-2 rounded-lg hover:bg-gray-50 transition-colors">
                  Back to List
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
}