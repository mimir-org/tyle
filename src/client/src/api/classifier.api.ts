import { apiClient } from "api/clients/apiClient";
import { RdlClassifier } from "types/common/rdlClassifier";
import { RdlClassifierRequest } from "types/common/rdlClassifierRequest";

const _basePath = "classifiers";

export const classifierApi = {
  getClassifiers() {
    return apiClient.get<RdlClassifier[]>(_basePath).then((r) => r.data);
  },
  getClassifier(id: number) {
    return apiClient.get<RdlClassifier>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postClassifier(item: RdlClassifierRequest) {
    return apiClient.post<RdlClassifier>(`${_basePath}`, item).then((r) => r.data);
  },
  deleteClassifier(id: number) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
