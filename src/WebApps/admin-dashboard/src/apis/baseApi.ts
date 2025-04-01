import { fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { RootState } from "../app/store";
import { logout, setTokens } from "../features/auth/authSlice";
import { RefreshTokenResponse } from "../features/auth/authTypes";

export const createBaseQuery = (baseUrl: string) => 
  fetchBaseQuery({
    baseUrl,
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).auth.accessToken;
      if (token) {
        headers.set("Authorization", `Bearer ${token}`);
      }
      return headers;
    },
  });

export const createBaseQueryWithRefreshToken = (baseUrl: string) => {
  const baseQuery = createBaseQuery(baseUrl);

  return async (args: any, api: any, extraOptions: any) => {
    let result = await baseQuery(args, api, extraOptions);

    if (result.error?.status === 401) {
      const refreshResult = await baseQuery({ url: "/auth/refresh-token", method: "POST" }, api, extraOptions) as { data: RefreshTokenResponse };

      if (refreshResult.data) {
        api.dispatch(setTokens({
          accessToken: refreshResult.data.newAccessToken ?? "",
          refreshToken: refreshResult.data.newRefreshToken ?? "",
        }));

        result = await baseQuery(args, api, extraOptions);
      } else {
        api.dispatch(logout());
      }
    }

    return result;
  };
};
