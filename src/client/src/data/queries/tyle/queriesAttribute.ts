import { Aspect, AttributeLibAm, QuantityDatumType } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiAttribute } from "../../api/tyle/apiAttribute";

const keys = {
  allAttributes: ["attributes"] as const,
  attributeLists: () => [...keys.allAttributes, "list"] as const,
  attributeAspectList: (aspect: Aspect) => [...keys.attributeLists(), { aspect }] as const,
  allPredefined: ["attributesPredefined"] as const,
  predefinedLists: () => [...keys.allPredefined, "list"] as const,
  allAspect: ["attributesAspect"] as const,
  aspectLists: () => [...keys.allAspect, "list"] as const,
  allReference: ["attributesReference"] as const,
  referenceLists: () => [...keys.allReference, "list"] as const,
  attribute: (id?: string) => [...keys.attributeLists(), id] as const,
  allQuantityDatum: ["quantityDatum"] as const,
  quantityDatum: (datumType: QuantityDatumType) => [...keys.allQuantityDatum, datumType] as const,
};

export const useGetAttributes = () => useQuery(keys.attributeLists(), apiAttribute.getAttributes);

export const useGetAttribute = (id?: string) =>
  useQuery(keys.attribute(id), () => apiAttribute.getAttribute(id), { enabled: !!id, retry: false });

export const useCreateAttribute = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeLibAm) => apiAttribute.postLibraryAttribute(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.attributeLists()),
  });
};

export const useGetAttributesByAspect = (aspect: Aspect) =>
  useQuery(keys.attributeAspectList(aspect), () => apiAttribute.getAttributesByAspect(aspect));

export const useGetAttributesPredefined = () => useQuery(keys.predefinedLists(), apiAttribute.getAttributesPredefined);

export const useGetQuantityDatum = (datumType: QuantityDatumType) =>
  useQuery(keys.quantityDatum(datumType), () => apiAttribute.getQuantityDatum(datumType));

export const useGetAttributesReference = () => useQuery(keys.referenceLists(), apiAttribute.getAttributesReference);
