﻿import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import BasicLayout from "../layouts/BasicLayout";
import MoviePage from "../pages/Movie";
import EmployeePage from "../pages/Employee/Index";
import CustomerPage from "../pages/Customer/Index";
import TheaterPage from "../pages/Theater/Index";
import AuthenPage from "../pages/Authentication/AuthenPage"
import CommingSoonPage from "../pages/CommingSoonPage";
<<<<<<< HEAD
import BlogPage from "../pages/Blog/Index";
=======
>>>>>>> 960a83c (commit)

const RouterComponent = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<BasicLayout />}>
                    <Route index element={<CommingSoonPage />} />
<<<<<<< HEAD
                    <Route path="/blog" element={<BlogPage />} />
=======
>>>>>>> 960a83c (commit)
                    <Route path="/home" element={<CommingSoonPage />} />
                    <Route path="/lich-chieu" element={<CommingSoonPage />} />
                    <Route path="/rap-chieu" element={<TheaterPage />} />
                    <Route path="/phim-anh" element={<MoviePage />} />
                    <Route path="/khach-hang" element={<CustomerPage />} />
                    <Route path="/nhan-vien" element={<EmployeePage />} />
                </Route>
                <Route path="/dang-nhap" element={<AuthenPage />} />
            </Routes>
        </Router>
    );
};

export default RouterComponent;
