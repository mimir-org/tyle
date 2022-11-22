import { useQuery } from "@tanstack/react-query";
import { unitApi } from "external/sources/unit/unit.api";

const keys = {
  all: ["units"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetUnits = () => useQuery(keys.lists(), unitApi.getUnits);
