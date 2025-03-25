import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { MainLayout, AuthLayout } from "../layouts"
import { Home, Ecommerce } from "../pages/dashboard"
import { Login, Register } from "../pages/auth"

const router = createBrowserRouter([
  {
    path: "/",
    element: <AuthLayout />,
    children: [
      { path: "login", element: <Login /> },
      { path: "register", element: <Register /> },
    ],
  },
  {
    path: "/dashboard",
    element: <MainLayout />,
    children: [
      { index: true, element: <Home /> },
      { path: "ecommerce", element: <Ecommerce /> },
    ],
  },
]);

const AppRoutes = () => {
  return <RouterProvider router={router} />;
};

export default AppRoutes;
