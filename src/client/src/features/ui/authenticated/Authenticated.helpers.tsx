import { nodeFormRoutes } from "features/entities/node/NodeFormRoutes";
import { terminalFormRoutes } from "features/entities/terminal/TerminalFormRoutes";
import { exploreRoutes } from "features/explore/ExploreRoutes";
import { settingsRoutes } from "features/settings/SettingsRoutes";
import { AuthenticatedLayout } from "features/ui/authenticated/layout/AuthenticatedLayout";
import { ErrorMessage } from "features/ui/common/ErrorMessage";
import { useTranslation } from "react-i18next";
import { createBrowserRouter } from "react-router-dom";

export const useAuthenticatedRouter = () => {
  const { t } = useTranslation("ui");

  return createBrowserRouter([
    {
      path: "/",
      element: <AuthenticatedLayout />,
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
        ...exploreRoutes,
        ...nodeFormRoutes,
        ...terminalFormRoutes,
        settingsRoutes,
        {
          path: "*",
          element: (
            <ErrorMessage
              title={t("global.notFound.title")}
              subtitle={t("global.notFound.subtitle")}
              status={t("global.notFound.status")}
              linkText={t("global.notFound.link")}
              linkPath={"/"}
            />
          ),
        },
      ],
    },
  ]);
};
