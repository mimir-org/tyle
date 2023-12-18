import { ErrorMessage } from "@mimirorg/component-library";
import { attributeFormRoutes } from "components/AttributeForm/AttributeFormRoutes";
import { attributeGroupFormRoutes } from "components/AttributeGroupForm/AttributeGroupFormRoutes";
import { blockFormRoutes } from "components/BlockForm/BlockFormRoutes";
import { exploreRoutes } from "components/Explore/ExploreRoutes";
import { settingsRoutes } from "components/SettingsLayout/SettingsRoutes";
import { terminalFormRoutes } from "components/TerminalForm/TerminalFormRoutes";
import { createBrowserRouter } from "react-router-dom";
import AuthenticatedLayout from "./AuthenticatedLayout";

export const useAuthenticatedRouter = () => {
  return createBrowserRouter([
    {
      path: "/",
      element: <AuthenticatedLayout />,
      errorElement: (
        <ErrorMessage
          title="Oops!"
          subtitle="An error occurred when loading the page. Please contact support if the problem persists."
          status="Error: Client"
          linkText="Go back home"
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
              title="Oops!"
              subtitle="We can't seem to find the page you're looking for."
              status="Error code: 404"
              linkText="Go back home"
              linkPath={"/"}
            />
          ),
        },
      ],
    },
  ]);
};
