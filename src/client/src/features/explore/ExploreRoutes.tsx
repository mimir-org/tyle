import { Explore } from "features/explore/Explore";
import { RouteObject } from "react-router-dom";

export const exploreRoutes: RouteObject[] = [
  {
    path: "",
    element: <Explore />,
  },
];
