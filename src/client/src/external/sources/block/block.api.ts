import { BlockTypeRequest } from "common/types/blocks/blockTypeRequest";
import { BlockView } from "common/types/blocks/blockView";
import { apiClient } from "external/client/apiClient";

const _basePath = "blocks";

export const blockApi = {
  getBlocks() {
    return apiClient.get<BlockView[]>(_basePath).then((r) => r.data);
  },
  getBlock(id?: string) {
    return apiClient.get<BlockView>(`${_basePath}/${id}`).then((r) => r.data);
  },
  getLatestApprovedBlock(id?: string) {
    return apiClient.get<BlockView>(`${_basePath}/latest-approved/${id}`).then((r) => r.data);
  },
  postBlock(item: BlockTypeRequest) {
    return apiClient.post<BlockView>(_basePath, item).then((r) => r.data);
  },
  putBlock(item: BlockTypeRequest, id?: string) {
    return apiClient.put<BlockView>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  deleteBlock(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
