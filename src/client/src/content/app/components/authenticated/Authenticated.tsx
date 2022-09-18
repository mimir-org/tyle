import { RouterProvider } from "react-router-dom";
import { useAuthenticatedRouter } from "./Authenticated.helpers";

export const Authenticated = () => {
  const router = useAuthenticatedRouter();
  return <RouterProvider router={router} />;
};
