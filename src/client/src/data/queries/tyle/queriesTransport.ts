import { TransportLibAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiTransport } from "../../api/tyle/apiTransport";
import { UpdateEntity } from "../../types/updateEntity";

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

  return useMutation((item: UpdateEntity<TransportLibAm>) => apiTransport.putTransport(item.id, item), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.transport(unit.id)),
  });
};

export const useDeleteTransport = () => {
  const queryClient = useQueryClient();

  return useMutation((id: string) => apiTransport.deleteTransport(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
