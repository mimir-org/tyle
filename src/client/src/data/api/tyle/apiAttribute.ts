import {
  AttributeLibCm,
  AttributePredefinedLibCm,
  QuantityDatumCm,
  QuantityDatumType,
} from "@mimirorg/typelibrary-types";
import { apiClient } from "../apiClient";

const _basePath = "libraryattribute";

export const apiAttribute = {
  getAttributes() {
    return apiClient.get<AttributeLibCm[]>(_basePath).then((r) => r.data);
  },
  getAttributesPredefined() {
    return apiClient.get<AttributePredefinedLibCm[]>(`${_basePath}/predefined`).then((r) => r.data);
  },
  getQuantityDatum(datumType: QuantityDatumType) {
    return apiClient.get<QuantityDatumCm[]>(`${_basePath}/datum/${datumType}`).then((r) => r.data);
  },
};
