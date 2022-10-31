import { State, TransportLibAm } from "@mimirorg/typelibrary-types";
import { transportApi } from "external/sources/transport/transport.api";
import { useMutation, useQuery, useQueryClient } from "react-query";

const keys = {
  all: ["transports"] as const,
  lists: () => [...keys.all, "list"] as const,
  transport: (id?: string) => [...keys.all, id] as const,
};

export const useGetTransports = () => useQuery(keys.lists(), transportApi.getTransports);

export const useGetTransport = (id?: string) =>
  useQuery(keys.transport(id), () => transportApi.getTransport(id), { enabled: !!id, retry: false });

export const useCreateTransport = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TransportLibAm) => transportApi.postTransport(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateTransport = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TransportLibAm) => transportApi.putTransport(item), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.transport(unit.id)),
  });
};

export const usePatchTransportState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => transportApi.patchTransportState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
