import { useQuery } from "react-query";
import { apiPurpose } from "../../api/tyle/apiPurpose";

const keys = {
  all: ["purposes"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetPurposes = () => useQuery(keys.lists(), apiPurpose.getPurposes);
