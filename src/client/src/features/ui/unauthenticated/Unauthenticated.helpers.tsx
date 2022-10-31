import { loginRoutes } from "features/auth/login/LoginRoutes";
import { recoverRoutes } from "features/auth/recover/RecoverRoutes";
import { registerRoutes } from "features/auth/register/RegisterRoutes";
import { ErrorMessage } from "features/ui/common/ErrorMessage";
import { UnauthenticatedLayout } from "features/ui/unauthenticated/layout/UnauthenticatedLayout";
import { useTranslation } from "react-i18next";
import { createBrowserRouter, Navigate } from "react-router-dom";

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
