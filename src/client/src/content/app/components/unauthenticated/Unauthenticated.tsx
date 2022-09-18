import { RouterProvider } from "react-router-dom";
import { useUnauthenticatedRouter } from "./Unauthenticated.helpers";

export const Unauthenticated = () => {
  const router = useUnauthenticatedRouter();
  return <RouterProvider router={router} />;
};
