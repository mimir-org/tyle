import { useQuery } from "react-query";
import { apiRds } from "../../api/tyle/apiRds";

const keys = {
  all: ["rds"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetRds = () => useQuery(keys.lists(), apiRds.getRds);
