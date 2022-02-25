import { apiClient } from "../apiClient";
import { AttributeAspectLibCm } from "../../../models/typeLibrary/client/attributeAspectLibCm";
import { AttributeAspectLibAm } from "../../../models/typeLibrary/application/attributeAspectLibAm";

const _basePath = "libraryattributeaspect";

export const apiAttributeAspect = {
  getAttributeAspects() {
    return apiClient.get<AttributeAspectLibCm[]>(_basePath).then((r) => r.data);
  },
  putAttributeAspect(id: string, item: AttributeAspectLibAm) {
    return apiClient.post<AttributeAspectLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postAttributeAspect(item: AttributeAspectLibAm) {
    return apiClient.post<AttributeAspectLibCm>(_basePath, item).then((r) => r.data);
  },
};
