import { MimirorgCompanyAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { companyApi } from "./company.api";
import { userKeys } from "./user.queries";

export const companyKeys = {
  all: ["companies"] as const,
  lists: () => [...companyKeys.all, "list"] as const,
  company: (id?: number) => [...companyKeys.all, { id }] as const,
  allCompanyUsersLists: [...userKeys.all, "list"] as const,
  companyUsers: (id?: string) => [...companyKeys.allCompanyUsersLists, { id }] as const,
  companyPendingUsers: () => [...companyKeys.allCompanyUsersLists, "pending"] as const,
};

export const useGetCompanies = () => useQuery(companyKeys.lists(), companyApi.getCompanies);

export const useGetCompany = (id?: number) =>
  useQuery(companyKeys.company(id), () => companyApi.getCompany(id), { enabled: !!id, retry: false });

export const useCreateCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((item: MimirorgCompanyAm) => companyApi.postCompany(item), {
    onSuccess: () => queryClient.invalidateQueries(companyKeys.lists()),
  });
};

export const useUpdateCompany = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation((update: MimirorgCompanyAm) => companyApi.putCompany(id, update), {
    onSuccess: (data) => queryClient.invalidateQueries(companyKeys.company(data.id)),
  });
};

export const useDeleteCompany = () => {
  const queryClient = useQueryClient();

  return useMutation((id: string) => companyApi.deleteCompany(id), {
    onSuccess: () => queryClient.invalidateQueries(companyKeys.lists()),
  });
};

export const useGetCompanyUsers = (companyId?: string) =>
  useQuery(companyKeys.companyUsers(companyId), () => companyApi.getCompanyUsers(companyId), {
    enabled: !!companyId,
    retry: false,
  });

export const useGetAuthCompanyUsers = (companyId?: string) =>
  useQuery(companyKeys.companyUsers(companyId), () => companyApi.getAuthCompanyUsers(companyId), {
    enabled: !!companyId,
    retry: false,
  });

export const useGetPendingUsers = () => useQuery(companyKeys.companyPendingUsers(), () => companyApi.getPendingUsers());
