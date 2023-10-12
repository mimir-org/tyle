import { RdlClassifier } from "common/types/common/rdlClassifier";
import { RdlClassifierRequest } from "common/types/common/rdlClassifierRequest";
import { apiClient } from "external/client/apiClient";

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
