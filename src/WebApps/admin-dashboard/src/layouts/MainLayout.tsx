import { Outlet } from "react-router-dom";
import { Header, Footer } from "../components/layout";

const MainLayout = () => {
  return (
    <>
      <Header />
      <main>
        <Outlet /> {/* Đây là nơi render các page */}
      </main>
      <Footer />
    </>
  );
};

export default MainLayout;
