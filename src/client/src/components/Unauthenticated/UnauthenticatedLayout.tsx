import { UnauthenticatedLayoutContainer } from "components/Unauthenticated/UnauthenticatedLayout.styled";
import { Outlet } from "react-router-dom";

export const UnauthenticatedLayout = () => (
  <UnauthenticatedLayoutContainer>
    <Outlet />
  </UnauthenticatedLayoutContainer>
);
