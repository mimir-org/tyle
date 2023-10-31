import {
  AuthenticatedContainer,
  AuthenticatedContentContainer,
} from "components/Authenticated/AuthenticatedLayout.styled";
import { Header } from "components/Header/Header";
import { Outlet } from "react-router-dom";

export const AuthenticatedLayout = () => (
  <AuthenticatedContainer>
    <Header />
    <AuthenticatedContentContainer>
      <Outlet />
    </AuthenticatedContentContainer>
  </AuthenticatedContainer>
);
