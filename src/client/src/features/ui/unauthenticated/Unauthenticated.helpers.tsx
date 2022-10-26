import { useTranslation } from "react-i18next";
import { createBrowserRouter, Navigate } from "react-router-dom";
import { loginRoutes } from "../../auth/login/LoginRoutes";
import { registerRoutes } from "../../auth/register/RegisterRoutes";
import { recoverRoutes } from "../../auth/restore/RecoverRoutes";
import { ErrorMessage } from "../common/ErrorMessage";
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
