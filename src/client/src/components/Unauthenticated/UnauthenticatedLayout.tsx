import { Outlet } from "react-router-dom";
import UnauthenticatedLayoutContainer from "./UnauthenticatedLayout.styled";

const UnauthenticatedLayout = () => (
  <UnauthenticatedLayoutContainer>
    <Outlet />
  </UnauthenticatedLayoutContainer>
);

export default UnauthenticatedLayout;
