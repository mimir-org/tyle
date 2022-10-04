import { State, TransportLibAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiTransport } from "../../api/tyle/apiTransport";

const keys = {
  all: ["transports"] as const,
  lists: () => [...keys.all, "list"] as const,
  transport: (id?: string) => [...keys.all, id] as const,
};

export const useGetTransports = () => useQuery(keys.lists(), apiTransport.getTransports);

export const useGetTransport = (id?: string) =>
  useQuery(keys.transport(id), () => apiTransport.getTransport(id), { enabled: !!id, retry: false });

export const useCreateTransport = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TransportLibAm) => apiTransport.postTransport(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateTransport = () => {
  const queryClient = useQueryClient();

  return useMutation((item: TransportLibAm) => apiTransport.putTransport(item), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.transport(unit.id)),
  });
};

export const usePatchTransportState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: string; state: State }) => apiTransport.patchTransportState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
