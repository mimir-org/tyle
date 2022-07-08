import { Route, Routes } from "react-router-dom";
import { NodeForm } from "../../../forms/node/NodeForm";
import { Header } from "../../../header/Header";
import { Home } from "../../../home";
import { AuthenticatedContainer, AuthenticatedContentContainer } from "./Authenticated.styled";

export const Authenticated = () => (
  <AuthenticatedContainer>
    <Header />
    <AuthenticatedContentContainer>
      <Routes>
        <Route path={"/"} element={<Home />}></Route>
        <Route path={"/form/node"} element={<NodeForm />}></Route>
        <Route path={"/form/node/clone/:id"} element={<NodeForm />}></Route>
        <Route path={"/form/node/edit/:id"} element={<NodeForm isEdit />}></Route>
      </Routes>
    </AuthenticatedContentContainer>
  </AuthenticatedContainer>
);
