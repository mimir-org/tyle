import { MimirorgCompanyAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiCompany } from "../../api/auth/apiCompany";
import { UpdateEntity } from "../../../common/types/updateEntity";

const keys = {
  all: ["companies"] as const,
  lists: () => [...keys.all, "list"] as const,
  company: (id?: number) => [...keys.all, { id }] as const,
};

export const useGetCompanies = () => useQuery(keys.lists(), apiCompany.getCompanies);

export const useGetCompany = (id?: number) =>
  useQuery(keys.company(id), () => apiCompany.getCompany(id), { enabled: !!id, retry: false });

export const useCreateCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgCompanyAm) => apiCompany.postCompany(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((update: UpdateEntity<MimirorgCompanyAm>) => apiCompany.putCompany(update.id, update), {
    onSuccess: (data) => queryClient.invalidateQueries(keys.company(data.id)),
  });
};

export const useDeleteCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((id: string) => apiCompany.deleteCompany(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
