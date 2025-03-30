import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { User, LoginRequest, LoginResponse, SignUpRequest, SignUpResponse } from "./authTypes";
import { RootState } from "../../app/store";

let local_base_url = "https://localhost:5056";
let docker_base_url = "https://localhost:6066";

export const authApi = createApi({
  reducerPath: "auth",
  baseQuery: fetchBaseQuery({ 
    baseUrl: local_base_url,
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).auth.accessToken;
      if (token) {
        headers.set("Authorization", `Bearer ${token}`);
      }
      return headers;
    },
  }),
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginRequest>({
      query: (credentials) => ({
        url: "/login",
        method: "POST",
        body: credentials,
      }),
    }),
    signup: builder.mutation<SignUpResponse, SignUpRequest>({
      query: (user) => ({
        url: '/signup',
        method: 'POST',
        body: user,
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