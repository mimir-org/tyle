import {
  AuthenticatedContainer,
  AuthenticatedContentContainer,
} from "features/ui/authenticated/layout/AuthenticatedLayout.styled";
import { Header } from "features/ui/header/Header";
import { Outlet } from "react-router-dom";

export const AuthenticatedLayout = () => (
  <AuthenticatedContainer>
    <Header />
    <AuthenticatedContentContainer>
      <Outlet />
    </AuthenticatedContentContainer>
  </AuthenticatedContainer>
);
