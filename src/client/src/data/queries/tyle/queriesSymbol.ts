import { useQuery } from "react-query";
import { apiSymbol } from "../../api/tyle/apiSymbol";

const keys = {
  all: ["symbols"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetSymbols = () => useQuery(keys.lists(), apiSymbol.getSymbols);
