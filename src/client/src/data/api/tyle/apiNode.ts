import { NodeLibAm, NodeLibCm, State } from "@mimirorg/typelibrary-types";
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
  putLibraryNode(item: NodeLibAm) {
    return apiClient.put<NodeLibCm>(_basePath, item).then((r) => r.data);
  },
  patchLibraryNodeState(id: string, state: State) {
    return apiClient.patch<NodeLibCm>(`${_basePath}/state/${id}`, state).then((r) => r.data);
  },
};
