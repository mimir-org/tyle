import { Outlet } from "react-router-dom";
import Header from "../Header";
import { AuthenticatedContainer, AuthenticatedContentContainer } from "./AuthenticatedLayout.styled";

const AuthenticatedLayout = () => (
  <AuthenticatedContainer>
    <Header />
    <AuthenticatedContentContainer>
      <Outlet />
    </AuthenticatedContentContainer>
  </AuthenticatedContainer>
);

export default AuthenticatedLayout;
