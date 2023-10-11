import { RdlPredicate } from "common/types/attributes/rdlPredicate";
import { RdlPredicateRequest } from "common/types/attributes/rdlPredicateRequest";
import { apiClient } from "external/client/apiClient";

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
