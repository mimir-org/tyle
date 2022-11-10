import { useQuery } from "@tanstack/react-query";
import { rdsApi } from "external/sources/rds/rds.api";

const keys = {
  all: ["rds"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetRds = () => useQuery(keys.lists(), rdsApi.getRds);
