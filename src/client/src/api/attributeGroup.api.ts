import { apiClient } from "api/clients/apiClient";
import { AttributeGroupRequest } from "types/attributes/attributeGroupRequest";
import { AttributeGroupView } from "types/attributes/attributeGroupView";

const _basePath = "attributegroups";

export const attributeGroupApi = {
  getAttributeGroups() {
    return apiClient.get<AttributeGroupView[]>(_basePath).then((r) => r.data);
  },
  getAttributeGroup(id: string) {
    return apiClient.get<AttributeGroupView>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postAttributeGroup(item: AttributeGroupRequest) {
    return apiClient.post<AttributeGroupView>(_basePath, item).then((r) => r.data);
  },
  putAttributeGroup(id: string, item: AttributeGroupRequest) {
    return apiClient.put<AttributeGroupView>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  deleteAttributeGroup(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};