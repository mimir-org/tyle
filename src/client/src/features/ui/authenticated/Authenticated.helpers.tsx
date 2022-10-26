import { useTranslation } from "react-i18next";
import { createBrowserRouter } from "react-router-dom";
import { exploreRoutes } from "../../explore/ExploreRoutes";
import { interfaceFormRoutes } from "../../../content/forms/interface/InterfaceFormRoutes";
import { nodeFormRoutes } from "../../../content/forms/node/NodeFormRoutes";
import { terminalFormRoutes } from "../../../content/forms/terminal/TerminalFormRoutes";
import { transportFormRoutes } from "../../../content/forms/transport/TransportFormRoutes";
import { ErrorMessage } from "../common/ErrorMessage";
import { AuthenticatedLayout } from "./layout/AuthenticatedLayout";

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
