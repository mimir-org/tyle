import { BlockLibAm, BlockLibCm, State, ApprovalDataCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryblock";

export const blockApi = {
  getBlocks() {
    return apiClient.get<BlockLibCm[]>(_basePath).then((r) => r.data);
  },
  getBlock(id?: string) {
    return apiClient.get<BlockLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  getLatestApprovedBlock(id?: string) {
    return apiClient.get<BlockLibCm>(`${_basePath}/latest-approved/${id}`).then((r) => r.data);
  },
  postBlock(item: BlockLibAm) {
    return apiClient.post<BlockLibCm>(_basePath, item).then((r) => r.data);
  },
  putBlock(item: BlockLibAm, id?: string) {
    return apiClient.put<BlockLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchBlockState(id: string, state: State) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  deleteBlock(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
