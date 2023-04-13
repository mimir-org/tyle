import {
  AttributeLibCm,
  AttributePredefinedLibCm,
  QuantityDatumLibCm,
  QuantityDatumType,
} from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryattribute";

export const attributeApi = {
  getAttributes() {
    return apiClient.get<AttributeLibCm[]>(_basePath).then((r) => r.data);
  },
  getAttributesPredefined() {
    return apiClient.get<AttributePredefinedLibCm[]>(`${_basePath}/predefined`).then((r) => r.data);
  },
  getQuantityDatum(datumType: QuantityDatumType) {
    return apiClient.get<QuantityDatumLibCm[]>(`${_basePath}/datum/${datumType}`).then((r) => r.data);
  },
};
