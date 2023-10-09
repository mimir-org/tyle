import {
  AttributePredefinedLibCm,
  State,
  ApprovalDataCm,
} from "@mimirorg/typelibrary-types";
import { AttributeTypeRequest } from "common/types/attributes/attributeTypeRequest";
import { AttributeView } from "common/types/attributes/attributeView";
import { apiClient } from "external/client/apiClient";

const _basePath = "attributes";

export const attributeApi = {
  getAttributes() {
    return apiClient.get<AttributeView[]>(_basePath).then((r) => r.data);
  },
  getAttribute(id?: string) {
    return apiClient.get<AttributeView>(`${_basePath}/${id}`).then((r) => r.data);
  },
  getAttributesPredefined() {
    return apiClient.get<AttributePredefinedLibCm[]>(`${_basePath}/predefined`).then((r) => r.data);
  },
  putAttribute(item: AttributeTypeRequest, id?: string) {
    return apiClient.put<AttributeView>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postAttribute(item: AttributeTypeRequest) {
    return apiClient.post<AttributeView>(`${_basePath}`, item).then((r) => r.data);
  },
  patchAttributeState(id: string, state: State) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  deleteAttribute(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
