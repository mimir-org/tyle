import { interfaceFormRoutes } from "features/entities/interface/InterfaceFormRoutes";
import { nodeFormRoutes } from "features/entities/node/NodeFormRoutes";
import { terminalFormRoutes } from "features/entities/terminal/TerminalFormRoutes";
import { transportFormRoutes } from "features/entities/transport/TransportFormRoutes";
import { exploreRoutes } from "features/explore/ExploreRoutes";
import { settingsRoutes } from "features/settings/SettingsRoutes";
import { AuthenticatedLayout } from "features/ui/authenticated/layout/AuthenticatedLayout";
import { ErrorMessage } from "features/ui/common/ErrorMessage";
import { useTranslation } from "react-i18next";
import { createBrowserRouter } from "react-router-dom";

export const useAuthenticatedRouter = () => {
  const { t } = useTranslation();

  return createBrowserRouter([
    {
      path: "/",
      element: <AuthenticatedLayout />,
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
        ...exploreRoutes,
        ...nodeFormRoutes,
        ...terminalFormRoutes,
        ...transportFormRoutes,
        ...interfaceFormRoutes,
        settingsRoutes,
        {
          path: "*",
          element: (
            <ErrorMessage
              title={t("notFound.title")}
              subtitle={t("notFound.subtitle")}
              status={t("notFound.status")}
              linkText={t("notFound.link")}
              linkPath={"/"}
            />
          ),
        },
      ],
    },
  ]);
};
