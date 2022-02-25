import { apiClient } from "../apiClient";
import { AttributeSourceLibCm } from "../../../models/typeLibrary/client/attributeSourceLibCm";
import { AttributeSourceLibAm } from "../../../models/typeLibrary/application/attributeSourceLibAm";

const _basePath = "libraryattributesource";

export const apiAttributeSource = {
  getAttributeSources() {
    return apiClient.get<AttributeSourceLibCm[]>(_basePath).then((r) => r.data);
  },
  putAttributeSource(id: string, item: AttributeSourceLibAm) {
    return apiClient.post<AttributeSourceLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postAttributeSource(item: AttributeSourceLibAm) {
    return apiClient.post<AttributeSourceLibCm>(_basePath, item).then((r) => r.data);
  },
};
