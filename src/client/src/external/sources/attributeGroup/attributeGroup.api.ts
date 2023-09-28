import { AttributeGroupLibCm, AttributeGroupLibAm, State, ApprovalDataCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryattributeGroup";

export const attributeGroupApi = {
  getAttributeGroups() {
    return apiClient.get<AttributeGroupLibCm[]>(_basePath).then((r) => r.data);
  },
  getAttributeGroup(id?: string) {
    return apiClient.get<AttributeGroupLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  putAttributeGroup(item: AttributeGroupLibAm, id?: string) {
    return apiClient.put<AttributeGroupLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postAttributeGroup(item: AttributeGroupLibAm) {
    return apiClient.post<AttributeGroupLibCm>(_basePath, item).then((r) => r.data);
  },
  deleteAttributeGroup(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
