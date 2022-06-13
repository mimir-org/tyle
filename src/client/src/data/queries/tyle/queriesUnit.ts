import { useQuery } from "react-query";
import { apiUnit } from "../../api/tyle/apiUnit";

const keys = {
  all: ["units"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetUnits = () => useQuery(keys.lists(), apiUnit.getUnits);
