import { MimirorgCompanyAm } from "@mimirorg/typelibrary-types";
import { UpdateEntity } from "common/types/updateEntity";
import { companyApi } from "external/sources/company/company.api";
import { useMutation, useQuery, useQueryClient } from "react-query";

const keys = {
  all: ["companies"] as const,
  lists: () => [...keys.all, "list"] as const,
  company: (id?: number) => [...keys.all, { id }] as const,
};

export const useGetCompanies = () => useQuery(keys.lists(), companyApi.getCompanies);

export const useGetCompany = (id?: number) =>
  useQuery(keys.company(id), () => companyApi.getCompany(id), { enabled: !!id, retry: false });

export const useCreateCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgCompanyAm) => companyApi.postCompany(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((update: UpdateEntity<MimirorgCompanyAm>) => companyApi.putCompany(update.id, update), {
    onSuccess: (data) => queryClient.invalidateQueries(keys.company(data.id)),
  });
};

export const useDeleteCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((id: string) => companyApi.deleteCompany(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
