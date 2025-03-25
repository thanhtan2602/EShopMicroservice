import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { User, LoginRequest, AuthResponse } from "../../types/auth/index";

export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery: fetchBaseQuery({ baseUrl: "/api/auth" }),
  endpoints: (builder) => ({
    login: builder.mutation<AuthResponse, LoginRequest>({
      query: (credentials) => ({
        url: "/login",
        method: "POST",
        body: credentials,
      }),
    }),
    logout: builder.mutation<void, void>({
      query: () => ({
        url: "/logout",
        method: "POST",
      }),
    }),
    getCurrentUser: builder.query<User, void>({
      query: () => "/me",
    }),
  }),
});

export const { useLoginMutation, useLogoutMutation, useGetCurrentUserQuery } = authApi;