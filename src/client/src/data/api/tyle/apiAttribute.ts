import {
  Aspect,
  QuantityDatumCm,
  AttributeLibAm,
  AttributeLibCm,
  AttributePredefinedLibCm,
  TypeReferenceCm,
  QuantityDatumType,
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
  getQuantityDatumRangeSpecifying() {
    return apiClient
      .get<QuantityDatumCm[]>(`${_basePath}/datum/${QuantityDatumType.QuantityDatumRangeSpecifying}`)
      .then((r) => r.data);
  },
  getQuantityDatumRegularitySpecified() {
    return apiClient
      .get<QuantityDatumCm[]>(`${_basePath}/datum/${QuantityDatumType.QuantityDatumRegularitySpecified}`)
      .then((r) => r.data);
  },
  getQuantityDatumSpecifiedProvenance() {
    return apiClient
      .get<QuantityDatumCm[]>(`${_basePath}/datum/${QuantityDatumType.QuantityDatumSpecifiedProvenance}`)
      .then((r) => r.data);
  },
  getQuantityDatumSpecifiedScope() {
    return apiClient
      .get<QuantityDatumCm[]>(`${_basePath}/datum/${QuantityDatumType.QuantityDatumSpecifiedScope}`)
      .then((r) => r.data);
  },
  getAttributesReference() {
    return apiClient.get<TypeReferenceCm[]>(`${_basePath}/reference`).then((r) => r.data);
  },
};
