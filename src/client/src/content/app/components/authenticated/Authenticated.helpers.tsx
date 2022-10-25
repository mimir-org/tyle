import { useTranslation } from "react-i18next";
import { createBrowserRouter } from "react-router-dom";
import { ErrorMessage } from "../../../common/error";
import { exploreRoutes } from "../../../explore/ExploreRoutes";
import { interfaceFormRoutes } from "../../../forms/interface/InterfaceFormRoutes";
import { nodeFormRoutes } from "../../../forms/node/NodeFormRoutes";
import { terminalFormRoutes } from "../../../forms/terminal/TerminalFormRoutes";
import { transportFormRoutes } from "../../../forms/transport/TransportFormRoutes";
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
