import { Route, Routes } from "react-router-dom";
import { Login } from "../../../forms/auth/login";
import { Register } from "../../../forms/auth/register";
import { UnauthenticatedLayout } from "./Unauthenticated.styled";

export const Unauthenticated = () => {
  return (
    <UnauthenticatedLayout>
      <Routes>
        <Route path={"/"} element={<Login />} />
        <Route path={"/register"} element={<Register />} />
      </Routes>
    </UnauthenticatedLayout>
  );
};
