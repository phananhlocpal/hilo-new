import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import BasicLayout from "../components/BasicLayout";
import HomePage from "../pages/HomePage";
import PostListPage from "../pages/PostListPage";
import BookingPage from "../pages/BookingPage";
import FilmDetail from "../pages/FilmDetail";
import TheaterPage from "../pages/TheaterPage";
<<<<<<< HEAD
import PaymentResult from "../pages/PaymentResult";
=======
>>>>>>> 960a83c (commit)

const RouterComponent = () => {
  return (
    <Router>
      <Routes>
      <Route path="/" element={<BasicLayout />}>
          <Route index element={<HomePage />} />
          <Route path="/dien-anh" element={<PostListPage />} />
          <Route path="/dat-ve" element={<BookingPage/>}/>
<<<<<<< HEAD
          <Route path="/ket-qua-dat-ve" element={<PaymentResult/>}/>
=======
>>>>>>> 960a83c (commit)
          <Route path="/dat-ve/:movieId" element={<BookingPage />} />
          <Route path="/chon-phim/:movieUrl" element={<FilmDetail />} />
          <Route path="/rap-chieu" element={<TheaterPage/>} />
        </Route>
      </Routes>
    </Router>
  );
};

export default RouterComponent;
