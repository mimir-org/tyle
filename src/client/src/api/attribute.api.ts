import { apiClient } from "api/clients/apiClient";
import { AttributeTypeRequest } from "types/attributes/attributeTypeRequest";
import { AttributeView } from "types/attributes/attributeView";
import { State } from "types/common/state";
import { StateChangeRequest } from "types/common/stateChangeRequest";

const _basePath = "attributes";

export const attributeApi = {
  getAttributes() {
    return apiClient.get<AttributeView[]>(_basePath).then((r) => r.data);
  },
  getAttributesByState(state: State) {
    return apiClient.get<AttributeView[]>(`${_basePath}?state=${state}`).then((r) => r.data);
  },
  getAttribute(id: string) {
    return apiClient.get<AttributeView>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postAttribute(item: AttributeTypeRequest) {
    return apiClient.post<AttributeView>(`${_basePath}`, item).then((r) => r.data);
  },
  putAttribute(id: string, item: AttributeTypeRequest) {
    return apiClient.put<AttributeView>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchAttributeState(id: string, item: StateChangeRequest) {
    return apiClient.patch(`${_basePath}/${id}/state`, item).then((r) => r.data);
  },
  deleteAttribute(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
