import { useUnauthenticatedRouter } from "components/Unauthenticated/Unauthenticated.helpers";
import { RouterProvider } from "react-router-dom";

export const Unauthenticated = () => {
  const router = useUnauthenticatedRouter();
  return <RouterProvider router={router} />;
};
