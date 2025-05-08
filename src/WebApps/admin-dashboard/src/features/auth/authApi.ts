import { createApi } from "@reduxjs/toolkit/query/react";
import { createBaseQueryWithRefreshToken } from "../../apis/baseApi";
import { 
  UserModel,
  LoginRequest,
  LoginResponse,
  SignUpRequest,
  SignUpResponse
} from "./authTypes";

const BASE_URL = process.env.NODE_ENV === "production" ? "https://localhost:6066/auth-service" : "https://localhost:5056/auth-service";

export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery: createBaseQueryWithRefreshToken(BASE_URL),
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
        url: "/register",
        method: "POST",
        body: user,
      }),
    }),
    logout: builder.mutation<void, void>({
      query: () => ({
        url: "/logout",
        method: "POST",
      }),
    }),
    getCurrentUser: builder.query<UserModel, void>({
      query: () => "/me",
    }),
  }),
});

export const { useLoginMutation, useLogoutMutation, useGetCurrentUserQuery, useSignupMutation } = authApi;
