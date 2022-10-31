import { symbolApi } from "external/sources/symbol/symbol.api";
import { useQuery } from "react-query";

const keys = {
  all: ["symbols"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetSymbols = () => useQuery(keys.lists(), symbolApi.getSymbols);
