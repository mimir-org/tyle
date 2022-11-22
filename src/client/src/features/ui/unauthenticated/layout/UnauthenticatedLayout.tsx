import { UnauthenticatedLayoutContainer } from "features/ui/unauthenticated/layout/UnauthenticatedLayout.styled";
import { Outlet } from "react-router-dom";

export const UnauthenticatedLayout = () => (
  <UnauthenticatedLayoutContainer>
    <Outlet />
  </UnauthenticatedLayoutContainer>
);
