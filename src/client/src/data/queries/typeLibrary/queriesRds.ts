import { useQuery } from "react-query";
import { apiRds } from "../../api/typeLibrary/apiRds";

const keys = {
  all: ["rds"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetRds = () => useQuery(keys.lists(), apiRds.getRds);
