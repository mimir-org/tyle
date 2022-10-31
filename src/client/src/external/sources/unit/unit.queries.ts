import { unitApi } from "external/sources/unit/unit.api";
import { useQuery } from "react-query";

const keys = {
  all: ["units"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetUnits = () => useQuery(keys.lists(), unitApi.getUnits);
