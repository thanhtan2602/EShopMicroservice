import { useEffect, useState } from "react";

const ThemeSwitcher = () => {
  const [theme, setTheme] = useState(localStorage.getItem("theme") || "light-theme");

  useEffect(() => {
    document.body.classList.remove("light-theme", "dark-theme");
    document.body.classList.add(theme);
    localStorage.setItem("theme", theme);
  }, [theme]);

  const toggleTheme = () => {
    setTheme(theme === "light-theme" ? "dark-theme" : "light-theme");
  };

  return (
    <button onClick={toggleTheme} className="btn">
      {theme === "light-theme" ? "🌙 Dark Mode" : "☀️ Light Mode"}
    </button>
  );
};

export default ThemeSwitcher;
