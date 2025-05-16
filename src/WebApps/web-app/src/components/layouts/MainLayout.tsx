import React from 'react';
import { Outlet } from 'react-router-dom';
import { Header, Footer } from "../layouts"

const MainLayout = () => {
  return (
    <div className="min-h-screen bg-gray-100">
      <Header />
      <main className="max-w-7xl mx-auto py-6 sm:px-6 lg:px-8">
        <Outlet />
      </main>
      <Footer />
    </div>
  );
};

export default MainLayout; 