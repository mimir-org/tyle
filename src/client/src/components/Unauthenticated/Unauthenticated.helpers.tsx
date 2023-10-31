import { loginRoutes } from "components/Login/LoginRoutes";
import { recoverRoutes } from "components/Recover/RecoverRoutes";
import { registerRoutes } from "components/Register/RegisterRoutes";
import { ErrorMessage } from "@mimirorg/component-library";
import { UnauthenticatedLayout } from "components/Unauthenticated/UnauthenticatedLayout";
import { useTranslation } from "react-i18next";
import { createBrowserRouter, Navigate } from "react-router-dom";

export const useUnauthenticatedRouter = () => {
  const { t } = useTranslation("ui");

  return createBrowserRouter([
    {
      path: "/",
      element: <UnauthenticatedLayout />,
      errorElement: (
        <ErrorMessage
          title={t("global.clientError.title")}
          subtitle={t("global.clientError.subtitle")}
          status={t("global.clientError.status")}
          linkText={t("global.clientError.link")}
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
