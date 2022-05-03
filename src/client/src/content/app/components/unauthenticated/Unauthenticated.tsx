import { Register } from "../../../forms/register";
import { Login } from "../../../forms/login";
import { Route, Routes } from "react-router-dom";
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
