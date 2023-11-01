import { useUnauthenticatedRouter } from "components/Unauthenticated/Unauthenticated.helpers";
import { RouterProvider } from "react-router-dom";

const Unauthenticated = () => {
  const router = useUnauthenticatedRouter();
  return <RouterProvider router={router} />;
};

export default Unauthenticated;
