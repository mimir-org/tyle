import { apiClient } from "../apiClient";
import { NodeLibCm } from "../../../models/tyle/client/nodeLibCm";
import { NodeLibAm } from "../../../models/tyle/application/nodeLibAm";

const _basePath = "librarynode";

export const apiNode = {
  getLibraryNodes() {
    return apiClient.get<NodeLibCm[]>(_basePath).then((r) => r.data);
  },
  getLibraryNode(id?: string) {
    return apiClient.get<NodeLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postLibraryNode(item: NodeLibAm) {
    return apiClient.post<NodeLibCm>(_basePath, item).then((r) => r.data);
  },
  putLibraryNode(item: NodeLibAm) {
    return apiClient.put<NodeLibCm>(_basePath, item).then((r) => r.data);
  },
  deleteLibraryNode(id: string) {
    return apiClient.delete<boolean>(`${_basePath}/${id}`).then((r) => r.data);
  },
};
