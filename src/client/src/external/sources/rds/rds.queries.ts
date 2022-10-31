import { rdsApi } from "external/sources/rds/rds.api";
import { useQuery } from "react-query";

const keys = {
  all: ["rds"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetRds = () => useQuery(keys.lists(), rdsApi.getRds);
