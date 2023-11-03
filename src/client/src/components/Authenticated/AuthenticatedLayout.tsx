import Header from "components/Header";
import { Outlet } from "react-router-dom";
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
