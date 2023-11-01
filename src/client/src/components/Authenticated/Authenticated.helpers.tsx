import { blockFormRoutes } from "components/BlockForm/BlockFormRoutes";
import { terminalFormRoutes } from "components/TerminalForm/TerminalFormRoutes";
import { exploreRoutes } from "components/Explore/ExploreRoutes";
import { settingsRoutes } from "components/SettingsLayout/SettingsRoutes";
import { AuthenticatedLayout } from "components/Authenticated/AuthenticatedLayout";
import { ErrorMessage } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { createBrowserRouter } from "react-router-dom";
import { attributeFormRoutes } from "../AttributeForm/AttributeFormRoutes";
import { attributeGroupFormRoutes } from "components/AttributeGroupForm/AttributeGroupFormRoutes";

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
        ...blockFormRoutes,
        ...terminalFormRoutes,
        ...attributeFormRoutes,
        ...attributeGroupFormRoutes,
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
