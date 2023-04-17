import { QuantityDatumType, AttributeLibAm } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { attributeApi } from "./attribute.api";

const keys = {
  allAttributes: ["attributes"] as const,
  attributeLists: () => [...keys.allAttributes, "list"] as const,
  allPredefined: ["attributesPredefined"] as const,
  predefinedLists: () => [...keys.allPredefined, "list"] as const,
  allQuantityDatum: ["quantityDatum"] as const,
  quantityDatum: (datumType: QuantityDatumType) => [...keys.allQuantityDatum, datumType] as const,
  attribute: (id?: string) => [...keys.attributeLists(), id] as const,
};

export const useGetAttributes = () => useQuery(keys.attributeLists(), attributeApi.getAttributes);

export const useGetAttributesPredefined = () => useQuery(keys.predefinedLists(), attributeApi.getAttributesPredefined);

export const useGetAttribute = (id?: string) =>
  useQuery(keys.attribute(id), () => attributeApi.getAttribute(id), { enabled: !!id, retry: false });

export const useGetQuantityDatum = (datumType: QuantityDatumType) =>
  useQuery(keys.quantityDatum(datumType), () => attributeApi.getQuantityDatum(datumType));

export const useUpdateAttributes = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeLibAm) => attributeApi.putAttribute(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.attribute(unit.id)),
  });
};
