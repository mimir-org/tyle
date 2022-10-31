import { useUnauthenticatedRouter } from "features/ui/unauthenticated/Unauthenticated.helpers";
import { RouterProvider } from "react-router-dom";

export const Unauthenticated = () => {
  const router = useUnauthenticatedRouter();
  return <RouterProvider router={router} />;
};
