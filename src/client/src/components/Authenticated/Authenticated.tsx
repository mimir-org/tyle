import { RouterProvider } from "react-router-dom";
import { useAuthenticatedRouter } from "./Authenticated.helpers";

const Authenticated = () => {
  const router = useAuthenticatedRouter();
  return <RouterProvider router={router} />;
};

export default Authenticated;
