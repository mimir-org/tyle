import { useTranslation } from "react-i18next";
import { createBrowserRouter } from "react-router-dom";
import { ErrorMessage } from "../../../common/error";
import { Explore } from "../../../explore";
import { AuthenticatedLayout } from "./layout/AuthenticatedLayout";
import { interfaceFormRoutes } from "./routes/InterfaceFormRoutes";
import { nodeFormRoutes } from "./routes/NodeFormRoutes";
import { terminalFormRoutes } from "./routes/TerminalFormRoutes";
import { transportFormRoutes } from "./routes/TransportFormRoutes";

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
        {
          path: "",
          element: <Explore />,
        },
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
