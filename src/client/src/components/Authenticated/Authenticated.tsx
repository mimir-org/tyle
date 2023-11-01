import { useAuthenticatedRouter } from "components/Authenticated/Authenticated.helpers";
import { RouterProvider } from "react-router-dom";

const Authenticated = () => {
  const router = useAuthenticatedRouter();
  return <RouterProvider router={router} />;
};

export default Authenticated;