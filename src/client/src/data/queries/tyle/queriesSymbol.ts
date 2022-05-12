import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiSymbol } from "../../api/tyle/apiSymbol";
import { SymbolLibAm } from "../../../models/tyle/application/symbolLibAm";

const keys = {
  all: ["symbols"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetSymbols = () => useQuery(keys.lists(), apiSymbol.getSymbols);

export const useCreateSymbol = () => {
  const queryClient = useQueryClient();

  return useMutation((item: SymbolLibAm) => apiSymbol.postSymbol(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
