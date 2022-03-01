import { apiClient } from "../apiClient";
import { AttributeQualifierLibCm } from "../../../models/typeLibrary/client/attributeQualifierLibCm";
import { AttributeQualifierLibAm } from "../../../models/typeLibrary/application/attributeQualifierLibAm";

const _basePath = "libraryattributequalifier";

export const apiAttributeQualifier = {
  getAttributeQualifiers() {
    return apiClient.get<AttributeQualifierLibCm[]>(_basePath).then((r) => r.data);
  },
  putAttributeQualifier(id: string, item: AttributeQualifierLibAm) {
    return apiClient.put<AttributeQualifierLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postAttributeQualifier(item: AttributeQualifierLibAm) {
    return apiClient.post<AttributeQualifierLibCm>(_basePath, item).then((r) => r.data);
  },
};
