import React, { useState } from "react";
import "./styles/_login.scss";
import { useDispatch } from "react-redux";
import { useLoginMutation } from "../../features/auth/authApi";
import { useNavigate } from "react-router-dom";
import { setTokens, setUser } from "../../features/auth/authSlice";
import { validateInput } from "../../features/auth/authHelper";

export default function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [login] = useLoginMutation();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  let coverImg = require("../../assets/images/login-images/login-cover.svg").default;
  const [passwordVisible, setPasswordVisible] = useState(false);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();

    const { isValid, errors } = validateInput({ email, password });
    if (!isValid) {
      console.log(errors);
      return;
    }

    try {
      const response = await login({ email, password }).unwrap();

      localStorage.setItem("user", JSON.stringify(response.user));
      dispatch(setUser(response.user));

      dispatch(setTokens({
        accessToken: response.accessToken ?? "",
        refreshToken: response.refreshToken ?? "",
      }));

      navigate('/dashboard');
    }
    catch (err: any) {
      if (err.status === 404) {
        alert("Sai tài khoản hoặc mật khẩu!");
      } else {
        console.error("Lỗi đăng nhập:", err);
      }
    }
  };

  const openGoogleLogin = () => {
    window.location.href = `https://localhost:5056/auth-service/google/signin`
  };

  return (
    <div className="mx-auto h-screen flex items-center">
      <div className="grid grid-cols-12 w-full rounded-lg overflow-hidden">
        <div className="hidden lg:flex items-center justify-center p-6 col-span-8">
          <img width={650} loading="lazy" src={coverImg} alt="cover" className="max-w-full" />
        </div>
        <div className="login-form flex flex-col justify-center p-16 w-full col-span-4">
          <div className="text-center mb-6">
            <img width={60} loading="lazy" src={coverImg} alt="logo" className="mx-auto w-16" />
            <h5 className="light-text text-xl font-semibold mt-2">Dashtrans Admin</h5>
            <p>Please log in to your account</p>
          </div>
          <form onSubmit={handleLogin} className="space-y-4" autoComplete="off">
            <div>
              <label htmlFor="input-email" className="block mb-2">Email</label>
              <input
                type="email"
                id="input-email"
                className="w-full focus:outline-none"
                placeholder="jhon@example.com"
                autoComplete="off"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
            <div>
              <label htmlFor="input-password" className="block mb-2">Password</label>
              <div className="relative">
                <input
                  type={passwordVisible ? "text" : "password"}
                  id="input-password"
                  className="w-full rounded px-3 py-2 focus:outline-none pr-10"
                  placeholder="Enter your password"
                  autoComplete="new-password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
                <button
                  type="button"
                  className="absolute inset-y-0 right-3 flex items-center"
                  onClick={() => setPasswordVisible(!passwordVisible)}
                >
                  <i className={passwordVisible ? "bx bx-show" : "bx bx-hide"}></i>
                </button>
              </div>
            </div>
            <div className="flex justify-between items-center text-sm my-4">
              <label className="flex items-center space-x-2">
                <input type="checkbox" id="input-remember" className="form-checkbox" />
                <span>Remember me</span>
              </label>
              <a href="#" className="light-text hover:underline">Forgot password ?</a>
            </div>
            <button className="light-text btn-signin w-full rounded transition">Sign In</button>
            <div className="mt-4 text-center">
              <span>Don't have an account yet?</span>
              <a href="/register" className="light-text hover:underline"> Sign up here</a>
            </div>
          </form>
          <div className="login-separater text-center mb-5">
            <span>OR SIGN IN WITH</span>
            <hr />
          </div>
          <div className="social-icons">
            <a href="https://www.facebook.com" aria-label="Facebook">
              <i className="bx bxl-facebook"></i>
            </a>
            <a href="https://x.com" aria-label="Twitter">
              <i className="bx bxl-twitter"></i>
            </a>
            <button onClick={openGoogleLogin} aria-label="Google">
              <i className="bx bxl-google"></i>
            </button>
            <a href="https://www.linkedin.com" aria-label="LinkedIn">
              <i className="bx bxl-linkedin"></i>
            </a>
          </div>
        </div>
      </div>
    </div>
  );
}
