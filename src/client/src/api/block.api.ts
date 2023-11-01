import { apiClient } from "api/clients/apiClient";
import { BlockTypeRequest } from "types/blocks/blockTypeRequest";
import { BlockView } from "types/blocks/blockView";
import { State } from "types/common/state";
import { StateChangeRequest } from "types/common/stateChangeRequest";

const _basePath = "blocks";

export const blockApi = {
  getBlocks() {
    return apiClient.get<BlockView[]>(_basePath).then((r) => r.data);
  },
  getBlocksByState(state: State) {
    return apiClient.get<BlockView[]>(`${_basePath}?state=${state}`).then((r) => r.data);
  },
  getBlock(id: string) {
    return apiClient.get<BlockView>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postBlock(item: BlockTypeRequest) {
    return apiClient.post<BlockView>(_basePath, item).then((r) => r.data);
  },
  putBlock(id: string, item: BlockTypeRequest) {
    return apiClient.put<BlockView>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchBlockState(id: string, item: StateChangeRequest) {
    return apiClient.patch(`${_basePath}/${id}/state`, item).then((r) => r.data);
  },
  deleteBlock(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
