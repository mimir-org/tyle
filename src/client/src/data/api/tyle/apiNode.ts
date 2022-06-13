import { NodeLibAm } from "../../../models/tyle/application/nodeLibAm";
import { NodeLibCm } from "../../../models/tyle/client/nodeLibCm";
import { apiClient } from "../apiClient";

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
  putLibraryNode(id: string, item: NodeLibAm) {
    return apiClient.put<NodeLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  deleteLibraryNode(id: string) {
    return apiClient.delete<boolean>(`${_basePath}/${id}`).then((r) => r.data);
  },
};
