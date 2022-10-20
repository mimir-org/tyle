import { useTranslation } from "react-i18next";
import { createBrowserRouter, Navigate } from "react-router-dom";
import { ErrorMessage } from "../../../common/error";
import { Login } from "../../../forms/auth/login";
import { Register } from "../../../forms/auth/register";
import { RegisterPath } from "../../../forms/auth/register/Register";
import { Recover } from "../../../forms/auth/restore";
import { RecoverPath } from "../../../forms/auth/restore/Recover";
import { UnauthenticatedLayout } from "./layout/UnauthenticatedLayout";

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
        { path: "*", element: <Navigate to={"/"} replace /> },
        { path: RegisterPath, element: <Register /> },
        { path: RecoverPath, element: <Recover /> },
      ],
    },
  ]);
};
