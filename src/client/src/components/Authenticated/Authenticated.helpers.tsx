import { ErrorMessage } from "@mimirorg/component-library";
import { useTranslation } from "react-i18next";
import { createBrowserRouter } from "react-router-dom";
import { attributeFormRoutes } from "../AttributeForm/AttributeFormRoutes";
import { attributeGroupFormRoutes } from "../AttributeGroupForm/AttributeGroupFormRoutes";
import { blockFormRoutes } from "../BlockForm/BlockFormRoutes";
import { exploreRoutes } from "../Explore/ExploreRoutes";
import { settingsRoutes } from "../SettingsLayout/SettingsRoutes";
import { terminalFormRoutes } from "../TerminalForm/TerminalFormRoutes";
import AuthenticatedLayout from "./AuthenticatedLayout";

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
