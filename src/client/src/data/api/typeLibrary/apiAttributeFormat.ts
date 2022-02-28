import { apiClient } from "../apiClient";
import { AttributeFormatLibCm } from "../../../models/typeLibrary/client/attributeFormatLibCm";
import { AttributeFormatLibAm } from "../../../models/typeLibrary/application/attributeFormatLibAm";

const _basePath = "libraryattributeformat";

export const apiAttributeFormat = {
  getAttributeFormats() {
    return apiClient.get<AttributeFormatLibCm[]>(_basePath).then((r) => r.data);
  },
  putAttributeFormat(id: string, item: AttributeFormatLibAm) {
    return apiClient.post<AttributeFormatLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postAttributeFormat(item: AttributeFormatLibAm) {
    return apiClient.post<AttributeFormatLibCm>(_basePath, item).then((r) => r.data);
  },
};
