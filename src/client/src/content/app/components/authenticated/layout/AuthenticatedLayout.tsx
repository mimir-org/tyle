import { Outlet } from "react-router-dom";
import { Header } from "../../../../header/Header";
import { AuthenticatedContainer, AuthenticatedContentContainer } from "./AuthenticatedLayout.styled";

export const AuthenticatedLayout = () => (
  <AuthenticatedContainer>
    <Header />
    <AuthenticatedContentContainer>
      <Outlet />
    </AuthenticatedContentContainer>
  </AuthenticatedContainer>
);
