import { MimirorgCompanyAm, MimirorgCompanyCm, MimirorgUserCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "mimirorgcompany";

export const companyApi = {
  getCompanies() {
    return apiClient.get<MimirorgCompanyCm[]>(_basePath).then((r) => r.data);
  },
  getCompany(id?: number) {
    return apiClient.get<MimirorgCompanyCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postCompany(item: MimirorgCompanyAm) {
    return apiClient.post<MimirorgCompanyCm>(_basePath, item).then((r) => r.data);
  },
  putCompany(id: string, item: MimirorgCompanyAm) {
    return apiClient.put<MimirorgCompanyCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  deleteCompany(id: string) {
    return apiClient.delete<boolean>(`${_basePath}/${id}`).then((r) => r.data);
  },
  getCompanyUsers(companyId?: string) {
    return apiClient.get<MimirorgUserCm[]>(`${_basePath}/${companyId}/users`).then((r) => r.data);
  },
  getAuthCompanyUsers(companyId?: string) {
    return apiClient.get<MimirorgUserCm[]>(`${_basePath}/${companyId}/authusers`).then((r) => r.data);
  },
  getPendingUsers() {
    return apiClient.get<MimirorgUserCm[]>(`${_basePath}/users/pending`).then((r) => r.data);
  },
};
