import { apiClient } from "../apiClient";
import { NodeLibCm } from "../../../models/typeLibrary/client/nodeLibCm";
import { NodeLibAm } from "../../../models/typeLibrary/application/nodeLibAm";

const _basePath = "librarynode";

export const apiNode = {
  getAspectNodes() {
    return apiClient.get<NodeLibCm[]>(_basePath).then((r) => r.data);
  },
  getAspectNode(id?: string) {
    return apiClient.get<NodeLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postAspectNode(item: NodeLibAm) {
    return apiClient.post<NodeLibCm>(_basePath, item).then((r) => r.data);
  },
};
