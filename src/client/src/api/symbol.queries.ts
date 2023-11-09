import { useQuery } from "@tanstack/react-query";
import { symbolApi } from "./symbol.api";

const symbolKeys = {
  all: ["symbols"] as const,
  lists: () => [...symbolKeys.all, "list"] as const,
  list: (filters: string) => [...symbolKeys.lists(), { filters }] as const,
  details: () => [...symbolKeys.all, "detail"] as const,
  detail: (id: number) => [...symbolKeys.details(), id] as const,
};

export const useGetSymbols = () => useQuery(symbolKeys.list(""), symbolApi.getSymbols);

export const useGetSymbol = (id: number) =>
  useQuery(symbolKeys.detail(id), () => symbolApi.getSymbol(id), { retry: false });
