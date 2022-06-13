import { useQuery } from "react-query";
import { apiRds } from "../../api/tyle/apiTerminal";

const keys = {
  all: ["terminals"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetTerminals = () => useQuery(keys.lists(), apiRds.getTerminals);
