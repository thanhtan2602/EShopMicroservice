import { createApi } from "@reduxjs/toolkit/query/react";
import { createBaseQueryWithRefreshToken } from "../../apis/baseApi";
import {
  CreateProductRequest,
  GetProductByIdResponse,
  GetProductsRequest,
  GetProductsResponse,
  Product,
  UpdateProductRequest
} from "./productType";

const BASE_URL = process.env.NODE_ENV === "production" ? "https://localhost:6060" : "https://localhost:5050";

export const productApi = createApi({
  reducerPath: "productApi",
  baseQuery: createBaseQueryWithRefreshToken(BASE_URL),
  endpoints: (builder) => ({
    getProducts: builder.query<GetProductsResponse, GetProductsRequest>({
      query: ({ pageNumber, pageSize }) =>
        `/products?pageNumber=${pageNumber}&pageSize=${pageSize}`,
    }),
    getProductById: builder.query<GetProductByIdResponse, string>({
      query: (id) => ({
        url: `/products/${id}`,
        method: "GET"
      }),
    }),
    addProduct: builder.mutation<string, CreateProductRequest>({
      query: (newProduct) => ({
        url: "/products",
        method: "POST",
        body: newProduct,
      }),
    }),
    updateProduct: builder.mutation<{ isSuccess: boolean }, UpdateProductRequest>({
      query: (updateData) => ({
        url: `/products`,
        method: "PUT",
        body: updateData,
      }),
    }),
    deleteProduct: builder.mutation<{ isSuccess: boolean }, number>({
      query: (id) => ({
        url: `/products/${id}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const { useGetProductsQuery, useGetProductByIdQuery, useAddProductMutation, useUpdateProductMutation, useDeleteProductMutation } = productApi;
