import ErrorMessage from "components/ErrorMessage";
import { loginRoutes } from "components/Login/LoginRoutes";
import { recoverRoutes } from "components/Recover/RecoverRoutes";
import { registerRoutes } from "components/Register/RegisterRoutes";
import { createBrowserRouter, Navigate } from "react-router-dom";
import UnauthenticatedLayout from "./UnauthenticatedLayout";

export const useUnauthenticatedRouter = () => {
  return createBrowserRouter([
    {
      path: "/",
      element: <UnauthenticatedLayout />,
      errorElement: (
        <ErrorMessage
          title="Oops!"
          subtitle="An error occurred when loading the page. Please contact support if the problem persists."
          status="Error: Client"
          linkText="Go back home"
          linkPath={"/"}
        />
      ),
      children: [
        ...loginRoutes,
        ...registerRoutes,
        ...recoverRoutes,
        { path: "*", element: <Navigate to={"/"} replace /> },
      ],
    },
  ]);
};
