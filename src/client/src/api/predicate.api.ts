import { apiClient } from "api/clients/apiClient";
import { RdlPredicate } from "types/attributes/rdlPredicate";
import { RdlPredicateRequest } from "types/attributes/rdlPredicateRequest";

const _basePath = "predicates";

export const predicateApi = {
  getPredicates() {
    return apiClient.get<RdlPredicate[]>(_basePath).then((r) => r.data);
  },
  getPredicate(id: number) {
    return apiClient.get<RdlPredicate>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postPredicate(item: RdlPredicateRequest) {
    return apiClient.post<RdlPredicate>(`${_basePath}`, item).then((r) => r.data);
  },
  deletePredicate(id: number) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
