import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

const baseQuery = fetchBaseQuery({
  baseUrl: 'http://localhost:5000/api', // Update this with your API base URL
  prepareHeaders: (headers) => {
    // You can add auth headers here if needed
    return headers;
  },
});

export const apiSlice = createApi({
  baseQuery,
  tagTypes: ['Product', 'Category', 'User', 'Order'],
  endpoints: () => ({}),
}); 