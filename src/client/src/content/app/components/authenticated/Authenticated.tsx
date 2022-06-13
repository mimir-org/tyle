import { Route, Routes } from "react-router-dom";
import { Box } from "../../../../complib/layouts";
import { NodeForm } from "../../../forms/node/NodeForm";
import { Header } from "../../../header/Header";
import { Home } from "../../../home";

export const Authenticated = () => (
  <Box display={"flex"} flexDirection={"column"} height={"100%"}>
    <Header />
    <Routes>
      <Route path={"/"} element={<Home />}></Route>
      <Route path={"/form/node"} element={<NodeForm />}></Route>
      <Route path={"/form/node/clone/:id"} element={<NodeForm />}></Route>
      <Route path={"/form/node/edit/:id"} element={<NodeForm isEdit />}></Route>
    </Routes>
  </Box>
);
