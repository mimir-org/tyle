import { RouterProvider } from "react-router-dom";
import { useUnauthenticatedRouter } from "./Unauthenticated.helpers";

const Unauthenticated = () => {
  const router = useUnauthenticatedRouter();
  return <RouterProvider router={router} />;
};

export default Unauthenticated;
