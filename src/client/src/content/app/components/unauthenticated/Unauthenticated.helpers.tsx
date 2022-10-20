import { useTranslation } from "react-i18next";
import { createBrowserRouter, Navigate } from "react-router-dom";
import { ErrorMessage } from "../../../common/error";
import { Login } from "../../../forms/auth/login";
import { Register } from "../../../forms/auth/register";
import { UnauthenticatedLayout } from "./layout/UnauthenticatedLayout";
import { RegisterPath } from "../../../forms/auth/register/Register";

export const useUnauthenticatedRouter = () => {
  const { t } = useTranslation();

  return createBrowserRouter([
    {
      path: "/",
      element: <UnauthenticatedLayout />,
      errorElement: (
        <ErrorMessage
          title={t("clientError.title")}
          subtitle={t("clientError.subtitle")}
          status={t("clientError.status")}
          linkText={t("clientError.link")}
          linkPath={"/"}
        />
      ),
      children: [
        { path: "", element: <Login /> },
        { path: RegisterPath, element: <Register /> },
        { path: "*", element: <Navigate to={"/"} replace /> },
      ],
    },
  ]);
};
