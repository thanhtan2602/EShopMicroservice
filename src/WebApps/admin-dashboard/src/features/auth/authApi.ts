import { createApi } from "@reduxjs/toolkit/query/react";
import { createBaseQueryWithRefreshToken } from "../../apis/baseApi";
import { 
  UserModel,
  LoginRequest,
  LoginResponse,
  SignUpRequest,
  SignUpResponse
} from "./authTypes";

const BASE_URL = process.env.NODE_ENV === "production" ? "https://localhost:6066" : "https://localhost:5056";

export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery: createBaseQueryWithRefreshToken(BASE_URL),
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginRequest>({
      query: (credentials) => ({
        url: "/auth/login",
        method: "POST",
        body: credentials,
      }),
    }),
    signup: builder.mutation<SignUpResponse, SignUpRequest>({
      query: (user) => ({
        url: "/auth/register",
        method: "POST",
        body: user,
      }),
    }),
    logout: builder.mutation<void, void>({
      query: () => ({
        url: "/auth/logout",
        method: "POST",
      }),
    }),
    getCurrentUser: builder.query<UserModel, void>({
      query: () => "/auth/me",
    }),
  }),
});

export const { useLoginMutation, useLogoutMutation, useGetCurrentUserQuery, useSignupMutation } = authApi;
