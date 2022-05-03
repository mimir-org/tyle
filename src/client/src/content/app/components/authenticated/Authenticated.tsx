import { Route, Routes } from "react-router-dom";
import { Box } from "../../../../complib/layouts";
import { Home } from "../../../home";
import { Header } from "../../../header/Header";

export const Authenticated = () => (
  <Box display={"flex"} flexDirection={"column"} height={"100%"}>
    <Header />
    <Routes>
      <Route path={"/"} element={<Home />}></Route>
    </Routes>
  </Box>
);
