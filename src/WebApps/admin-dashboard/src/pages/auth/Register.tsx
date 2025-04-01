import React, { useState } from "react";
import "./styles/_login.scss";
import { useSignupMutation } from "../../features/auth/authApi";
import { useNavigate } from "react-router-dom";
import { validateInput } from "../../features/auth/authHelper";

export default function Register() {
  const [fullName, setFullName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [signup] = useSignupMutation();
  const navigate = useNavigate();

  let coverImg = require("../../assets/images/login-images/register-cover.svg").default;
  const [passwordVisible, setPasswordVisible] = useState(false);
  const [confirmPasswordVisible, setConfirmPasswordVisible] = useState(false);

  const handleSignUp = async (e: React.FormEvent) => {
    e.preventDefault();

    const { isValid, errors } = validateInput({ email, password, confirmPassword });
    if (!isValid) {
      console.log(errors);
      return;
    }

    try {
      await signup({ fullName, email, password }).unwrap();
      navigate('/login');
    } catch (err: any) {
      console.error(err);
    }
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
            <p>Please fill the below details to create your account</p>
          </div>
          <form onSubmit={handleSignUp} className="space-y-4" autoComplete="off">
            <div>
              <label htmlFor="input-fullName" className="block mb-2">Full Name</label>
              <input
                type="text"
                id="input-fullName"
                className="w-full focus:outline-none"
                autoComplete="off"
                value={fullName}
                onChange={(e) => setFullName(e.target.value)}
              />
            </div>
            <div>
              <label htmlFor="input-email" className="block mb-2">Email Address</label>
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
            <div>
              <label htmlFor="input-confirmPassword" className="block mb-2">Confirm Password</label>
              <div className="relative">
                <input
                  type={confirmPasswordVisible ? "text" : "password"}
                  id="input-confirmPassword"
                  className="w-full rounded px-3 py-2 focus:outline-none pr-10"
                  placeholder="Re-Enter your password"
                  autoComplete="new-confirmPassword"
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                />
                <button
                  type="button"
                  className="absolute inset-y-0 right-3 flex items-center"
                  onClick={() => setConfirmPasswordVisible(!confirmPasswordVisible)}
                >
                  <i className={confirmPasswordVisible ? "bx bx-show" : "bx bx-hide"}></i>
                </button>
              </div>
            </div>
            <button className="light-text btn-signin w-full rounded transition">Sign up</button>
            <div className="mt-4 text-center">
              <span>Already have an account?</span>
              <a href="/register" className="light-text hover:underline"> Sign in here</a>
            </div>
          </form>
          <div className="login-separater text-center mb-5">
            <span>OR SIGN UP WITH EMAIL</span>
            <hr />
          </div>
          <div className="social-icons">
            <a href="https://www.facebook.com" aria-label="Facebook">
              <i className="bx bxl-facebook"></i>
            </a>
            <a href="https://x.com" aria-label="Twitter">
              <i className="bx bxl-twitter"></i>
            </a>
            <a href="https://www.google.com" aria-label="Google">
              <i className="bx bxl-google"></i>
            </a>
            <a href="https://www.linkedin.com" aria-label="LinkedIn">
              <i className="bx bxl-linkedin"></i>
            </a>
          </div>
        </div>
      </div>
    </div>
  );
}
