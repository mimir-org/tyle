import { Outlet } from "react-router-dom";
import { UnauthenticatedLayoutContainer } from "./UnauthenticatedLayout.styled";

export const UnauthenticatedLayout = () => (
  <UnauthenticatedLayoutContainer>
    <Outlet />
  </UnauthenticatedLayoutContainer>
);
