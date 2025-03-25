import { Outlet } from "react-router-dom";

const AuthLayout = () => {
  return (
    <div className="auth-container">
      <Outlet /> {/* Render Login/Register */}
    </div>
  );
};

export default AuthLayout;
