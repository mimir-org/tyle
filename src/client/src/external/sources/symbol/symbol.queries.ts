import { useQuery } from "@tanstack/react-query";
import { symbolApi } from "external/sources/symbol/symbol.api";

const keys = {
  all: ["symbols"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetSymbols = () => useQuery(keys.lists(), symbolApi.getSymbols);
