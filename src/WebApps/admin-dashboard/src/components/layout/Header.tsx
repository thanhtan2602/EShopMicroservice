import { useState } from "react";

function Header() {
  return (
    <header className="h-16 bg-white shadow-md fixed top-0 right-0 left-64 z-10">
      <div className="flex items-center justify-between h-full px-6">
        <h1 className="text-xl font-semibold text-gray-800">REACT COURSE</h1>
        <div className="flex items-center space-x-4">
          <button className="p-2 hover:bg-gray-100 rounded-full">
            <span className="sr-only">Notifications</span>
            🔔
          </button>
          <button className="p-2 hover:bg-gray-100 rounded-full">
            <span className="sr-only">Profile</span>
            👤
          </button>
        </div>
      </div>
    </header>
  );
}

export default Header;