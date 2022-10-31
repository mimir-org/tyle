import { useAuthenticatedRouter } from "features/ui/authenticated/Authenticated.helpers";
import { RouterProvider } from "react-router-dom";

export const Authenticated = () => {
  const router = useAuthenticatedRouter();
  return <RouterProvider router={router} />;
};
