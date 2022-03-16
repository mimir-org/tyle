import { Route, Routes } from "react-router-dom";
import { Home } from "../../../home";

export const Authenticated = () => {
  return (
    <Routes>
      <Route path={"/"} element={<Home />}></Route>
    </Routes>
  );
};
