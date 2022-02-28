import { apiClient } from "../apiClient";
import { AttributeLibCm } from "../../../models/typeLibrary/client/attributeLibCm";
import { AttributeLibAm } from "../../../models/typeLibrary/application/attributeLibAm";
import { AttributePredefinedLibCm } from "../../../models/typeLibrary/client/attributePredefinedLibCm";
import { Aspect } from "../../../models/typeLibrary/enums/aspect";

const _basePath = "libraryattribute";

export const apiAttribute = {
  getAttributes() {
    return apiClient.get<AttributeLibCm[]>(_basePath).then((r) => r.data);
  },
  getAttributesByAspect(aspect: Aspect) {
    return apiClient.get<AttributeLibCm[]>(`${_basePath}/${aspect}`).then((r) => r.data);
  },
  postAttribute(item: AttributeLibAm) {
    return apiClient.post<AttributeLibCm>(_basePath, item).then((r) => r.data);
  },
  getAttributesPredefined() {
    return apiClient.get<AttributePredefinedLibCm[]>(`${_basePath}/predefined`).then((r) => r.data);
  },
};
