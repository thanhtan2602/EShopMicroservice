import React from 'react';
import {
  Row,
} from 'react-bootstrap';
import ProductCard from './ProductCard';
import { PagingInfo } from 'types/pagingTypes';
import { Product } from 'types/productTypes';

interface ProductListProps {
  products: Product[];
  pagingInfo: PagingInfo;
  onPageChange: (page: number) => void;
}

const ProductList: React.FC<ProductListProps> = ({ products, pagingInfo, onPageChange }) => {

  return (
    <div className="product-list">
      <Row className="grid grid-col-2 lg:grid-cols-5 gap-8">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} /> 
        ))}
      </Row>
    </div>
  );
};

export default ProductList;