import { Outlet } from "react-router-dom";
import { UnauthenticatedContainer, UnauthenticatedContentContainer } from "./UnauthenticatedLayout.styled";

export const UnauthenticatedLayout = () => (
  <UnauthenticatedContainer>
    <UnauthenticatedContentContainer>
      <Outlet />
    </UnauthenticatedContentContainer>
  </UnauthenticatedContainer>
);
