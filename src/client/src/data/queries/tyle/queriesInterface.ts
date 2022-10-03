import { InterfaceLibAm, State } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiInterface } from "../../api/tyle/apiInterface";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["interfaces"] as const,
  lists: () => [...keys.all, "list"] as const,
  interface: (id?: string) => [...keys.all, id] as const,
};

export const useGetInterfaces = () => useQuery(keys.lists(), apiInterface.getInterfaces);

export const useGetInterface = (id?: string) =>
  useQuery(keys.interface(id), () => apiInterface.getInterface(id), { enabled: !!id, retry: false });

export const useCreateInterface = () => {
  const queryClient = useQueryClient();

  return useMutation((item: InterfaceLibAm) => apiInterface.postInterface(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateInterface = () => {
  const queryClient = useQueryClient();

  return useMutation((item: UpdateEntity<InterfaceLibAm>) => apiInterface.putInterface(item.id, item), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.interface(unit.id)),
  });
};

export const usePatchInterfaceState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => apiInterface.patchInterfaceState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
