import { Outlet } from "react-router-dom";
import { Header, Footer, SideBar } from "../components/layout";
import "./styles/_mainLayout.scss";

const MainLayout = () => {
  return (
    <>
      <div className="wrapper">
        <div className="flex w-full h-screen">
          <SideBar />
          <div className="grow flex flex-col">
            <Header />
            <div className="page-wrapper grow ml-[256px]">
              <Outlet />
            </div>
            <Footer />
          </div>
        </div>
      </div>
    </>
  );
};

export default MainLayout;
