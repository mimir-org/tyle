import { InterfaceLibAm, State } from "@mimirorg/typelibrary-types";
import { interfaceApi } from "external/sources/interface/interface.api";
import { useMutation, useQuery, useQueryClient } from "react-query";

const keys = {
  all: ["interfaces"] as const,
  lists: () => [...keys.all, "list"] as const,
  interface: (id?: string) => [...keys.all, id] as const,
};

export const useGetInterfaces = () => useQuery(keys.lists(), interfaceApi.getInterfaces);

export const useGetInterface = (id?: string) =>
  useQuery(keys.interface(id), () => interfaceApi.getInterface(id), { enabled: !!id, retry: false });

export const useCreateInterface = () => {
  const queryClient = useQueryClient();

  return useMutation((item: InterfaceLibAm) => interfaceApi.postInterface(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateInterface = () => {
  const queryClient = useQueryClient();

  return useMutation((item: InterfaceLibAm) => interfaceApi.putInterface(item), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.interface(unit.id)),
  });
};

export const usePatchInterfaceState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => interfaceApi.patchInterfaceState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
