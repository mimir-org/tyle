import { BlockTypeRequest } from "common/types/blocks/blockTypeRequest";
import { BlockView } from "common/types/blocks/blockView";
import { State } from "common/types/common/state";
import { StateChangeRequest } from "common/types/common/stateChangeRequest";
import { apiClient } from "external/client/apiClient";

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
