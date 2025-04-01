import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { MainLayout, AuthLayout } from "../layouts"
import { Home, Ecommerce } from "../pages/dashboard"
import { Login, Register } from "../pages/auth"
import { ProductDetail, ProductList } from "../pages/eCommerce";

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
  {
    path: "/ecommerce",
    element: <MainLayout />,
    children: [
      { path: "product-list", element: <ProductList /> },
      { path: "product-detail/:productId", element: <ProductDetail /> },
    ],
  },
]);

const AppRoutes = () => {
  return <RouterProvider router={router} />;
};

export default AppRoutes;
