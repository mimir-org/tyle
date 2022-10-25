import { useTranslation } from "react-i18next";
import { createBrowserRouter, Navigate } from "react-router-dom";
import { ErrorMessage } from "../../../common/error";
import { loginRoutes } from "../../../forms/auth/login/LoginRoutes";
import { registerRoutes } from "../../../forms/auth/register/RegisterRoutes";
import { recoverRoutes } from "../../../forms/auth/restore/RecoverRoutes";
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
        ...loginRoutes,
        ...registerRoutes,
        ...recoverRoutes,
        { path: "*", element: <Navigate to={"/"} replace /> },
      ],
    },
  ]);
};
