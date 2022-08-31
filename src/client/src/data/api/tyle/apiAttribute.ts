import {
  Aspect,
  AttributeConditionLibCm,
  AttributeFormatLibCm,
  AttributeLibAm,
  AttributeLibCm,
  AttributePredefinedLibCm,
  AttributeQualifierLibCm,
  AttributeSourceLibCm,
  TypeReferenceCm,
} from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "libraryattribute";

export const apiAttribute = {
  getAttributes() {
    return apiClient.get<AttributeLibCm[]>(_basePath).then((r) => r.data);
  },
  getAttribute(id?: string) {
    return apiClient.get<AttributeLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postLibraryAttribute(item: AttributeLibAm) {
    return apiClient.post<AttributeLibCm>(_basePath, item).then((r) => r.data);
  },
  getAttributesByAspect(aspect: Aspect) {
    return apiClient.get<AttributeLibCm[]>(`${_basePath}/aspect/${aspect}`).then((r) => r.data);
  },
  getAttributesPredefined() {
    return apiClient.get<AttributePredefinedLibCm[]>(`${_basePath}/predefined`).then((r) => r.data);
  },
  getAttributesCondition() {
    return apiClient.get<AttributeConditionLibCm[]>(`${_basePath}/condition`).then((r) => r.data);
  },
  getAttributesFormat() {
    return apiClient.get<AttributeFormatLibCm[]>(`${_basePath}/format`).then((r) => r.data);
  },
  getAttributesQualifier() {
    return apiClient.get<AttributeQualifierLibCm[]>(`${_basePath}/qualifier`).then((r) => r.data);
  },
  getAttributesSource() {
    return apiClient.get<AttributeSourceLibCm[]>(`${_basePath}/source`).then((r) => r.data);
  },
  getAttributesReference() {
    return apiClient.get<TypeReferenceCm[]>(`${_basePath}/reference`).then((r) => r.data);
  },
};
