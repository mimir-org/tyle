import { useQuery } from "react-query";
import { Aspect } from "../../../models/tyle/enums/aspect";
import { apiAttribute } from "../../api/tyle/apiAttribute";

const keys = {
  allAttributes: ["attributes"] as const,
  attributeLists: () => [...keys.allAttributes, "list"] as const,
  attributeAspectList: (aspect: Aspect) => [...keys.attributeLists(), { aspect }] as const,
  allPredefined: ["attributesPredefined"] as const,
  predefinedLists: () => [...keys.allPredefined, "list"] as const,
  allAspect: ["attributesAspect"] as const,
  aspectLists: () => [...keys.allAspect, "list"] as const,
  allCondition: ["attributesCondition"] as const,
  conditionLists: () => [...keys.allCondition, "list"] as const,
  allFormat: ["attributesFormat"] as const,
  formatLists: () => [...keys.allFormat, "list"] as const,
  allQualifier: ["attributesQualifier"] as const,
  qualifierLists: () => [...keys.allQualifier, "list"] as const,
  allSource: ["attributesSource"] as const,
  sourceLists: () => [...keys.allSource, "list"] as const,
};

export const useGetAttributes = () => useQuery(keys.attributeLists(), apiAttribute.getAttributes);

export const useGetAttributesByAspect = (aspect: Aspect) =>
  useQuery(keys.attributeAspectList(aspect), () => apiAttribute.getAttributesByAspect(aspect));

export const useGetAttributesPredefined = () => useQuery(keys.predefinedLists(), apiAttribute.getAttributesPredefined);

export const useGetAttributesAspect = () => useQuery(keys.aspectLists(), apiAttribute.getAttributesAspect);

export const useGetAttributesCondition = () => useQuery(keys.conditionLists(), apiAttribute.getAttributesCondition);

export const useGetAttributesFormat = () => useQuery(keys.formatLists(), apiAttribute.getAttributesFormat);

export const useGetAttributesQualifier = () => useQuery(keys.qualifierLists(), apiAttribute.getAttributesQualifier);

export const useGetAttributesSource = () => useQuery(keys.sourceLists(), apiAttribute.getAttributesSource);
