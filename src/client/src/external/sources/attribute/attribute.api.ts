import {
  AttributeLibCm,
  AttributePredefinedLibCm,
  QuantityDatumLibCm,
  QuantityDatumType,
  AttributeLibAm,
  State,
} from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryattribute";

export const attributeApi = {
  getAttributes() {
    return apiClient.get<AttributeLibCm[]>(_basePath).then((r) => r.data);
  },
  getAttribute(id?: string) {
    return apiClient.get<AttributeLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  getAttributesPredefined() {
    return apiClient.get<AttributePredefinedLibCm[]>(`${_basePath}/predefined`).then((r) => r.data);
  },
  putAttribute(item: AttributeLibAm, id?: string) {
    return apiClient.put<AttributeLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postAttribute(item: AttributeLibAm) {
    return apiClient.post<AttributeLibCm>(`${_basePath}`, item).then((r) => r.data);
  },
  patchAttributeState(id: string, state: State) {
    return apiClient.patch<AttributeLibCm>(`${_basePath}/${id}/state`, { state }).then((r) => r.data);
  },
};
