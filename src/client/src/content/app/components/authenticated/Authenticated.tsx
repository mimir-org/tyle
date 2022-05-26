import { Route, Routes } from "react-router-dom";
import { Box } from "../../../../complib/layouts";
import { Home } from "../../../home";
import { Header } from "../../../header/Header";
import { NodeForm } from "../../../forms/node/NodeForm";

export const Authenticated = () => (
  <Box display={"flex"} flexDirection={"column"} height={"100%"}>
    <Header />
    <Routes>
      <Route path={"/"} element={<Home />}></Route>
      <Route path={"/nodeform"} element={<NodeForm />}></Route>
    </Routes>
  </Box>
);
