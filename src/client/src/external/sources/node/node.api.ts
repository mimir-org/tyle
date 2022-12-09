import { NodeLibAm, NodeLibCm, State, ApprovalDataCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "librarynode";

export const nodeApi = {
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
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  patchLibraryNodeStateReject(id: string) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/reject`).then((r) => r.data);
  },
};
