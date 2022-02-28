import { useQuery } from "react-query";
import { apiTransport } from "../../api/typeLibrary/apiTransport";

const keys = {
  all: ["transports"] as const,
  lists: () => [...keys.all, "list"] as const,
  transport: (id: string) => [...keys.all, id] as const,
};

export const useGetTransports = () => useQuery(keys.lists(), apiTransport.getTransports);

export const useGetTransport = (id: string) => useQuery(keys.transport(id), () => apiTransport.getTransport(id));
