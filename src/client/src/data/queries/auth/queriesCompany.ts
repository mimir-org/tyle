import { useMutation, useQuery, useQueryClient } from "react-query";
import { MimirorgCompanyAm } from "../../../models/auth/application/mimirorgCompanyAm";
import { apiCompany } from "../../api/auth/apiCompany";

const keys = {
  all: ["companies"] as const,
  lists: () => [...keys.all, "list"] as const,
  company: (id: number) => [...keys.lists(), { id }] as const,
};

export const useGetCompanies = () => useQuery(keys.lists(), apiCompany.getCompanies);

export const useGetCompany = (id: number) => useQuery(keys.company(id), () => apiCompany.getCompany(id));

export const useCreateCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgCompanyAm) => apiCompany.postCompany(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateCompany = () => {
  const queryClient = useQueryClient();

  return useMutation(
    (update: { item: MimirorgCompanyAm; id: number }) => apiCompany.putCompany(update.id, update.item),
    {
      onSuccess: (data) => queryClient.invalidateQueries(keys.company(data.id)),
    }
  );
};

export const useDeleteCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((id: number) => apiCompany.deleteCompany(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
