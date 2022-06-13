import { AttributeAspectLibCm } from "../../../models/tyle/client/attributeAspectLibCm";
import { AttributeConditionLibCm } from "../../../models/tyle/client/attributeConditionLibCm";
import { AttributeFormatLibCm } from "../../../models/tyle/client/attributeFormatLibCm";
import { AttributeLibCm } from "../../../models/tyle/client/attributeLibCm";
import { AttributePredefinedLibCm } from "../../../models/tyle/client/attributePredefinedLibCm";
import { AttributeQualifierLibCm } from "../../../models/tyle/client/attributeQualifierLibCm";
import { AttributeSourceLibCm } from "../../../models/tyle/client/attributeSourceLibCm";
import { Aspect } from "../../../models/tyle/enums/aspect";
import { apiClient } from "../apiClient";

const _basePath = "libraryattribute";

export const apiAttribute = {
  getAttributes() {
    return apiClient.get<AttributeLibCm[]>(_basePath).then((r) => r.data);
  },
  getAttributesByAspect(aspect: Aspect) {
    return apiClient.get<AttributeLibCm[]>(`${_basePath}/${aspect}`).then((r) => r.data);
  },
  getAttributesPredefined() {
    return apiClient.get<AttributePredefinedLibCm[]>(`${_basePath}/predefined`).then((r) => r.data);
  },
  getAttributesAspect() {
    return apiClient.get<AttributeAspectLibCm[]>(`${_basePath}/aspect`).then((r) => r.data);
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
};
