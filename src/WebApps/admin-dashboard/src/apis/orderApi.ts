import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { order } from "../types/order/index";

export const orderApi = createApi({
  reducerPath: "orderApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "https://yourapi.com/api/",
    prepareHeaders: (headers) => {
      const token = localStorage.getItem("token");
      if (token) headers.set("Authorization", `Bearer ${token}`);
      return headers;
    },
  }),
  tagTypes: ["Orders"],
  endpoints: (builder) => ({
    getAllOrders: builder.query<order[], void>({
      query: () => "orders",
      providesTags: ["Orders"],
    }),
    createOrder: builder.mutation({
      query: (order) => ({
        url: "orders",
        method: "POST",
        body: order,
      }),
      invalidatesTags: ["Orders"],
    }),
  }),
});

export const { useGetAllOrdersQuery, useCreateOrderMutation } = orderApi;
