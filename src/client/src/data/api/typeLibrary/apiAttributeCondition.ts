import { apiClient } from "../apiClient";
import { AttributeConditionLibCm } from "../../../models/typeLibrary/client/attributeConditionLibCm";
import { AttributeConditionLibAm } from "../../../models/typeLibrary/application/attributeConditionLibAm";

const _basePath = "libraryattributecondition";

export const apiAttributeCondition = {
  getAttributeConditions() {
    return apiClient.get<AttributeConditionLibCm[]>(_basePath).then((r) => r.data);
  },
  putAttributeCondition(id: string, item: AttributeConditionLibAm) {
    return apiClient.put<AttributeConditionLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postAttributeCondition(item: AttributeConditionLibAm) {
    return apiClient.post<AttributeConditionLibCm>(_basePath, item).then((r) => r.data);
  },
};
