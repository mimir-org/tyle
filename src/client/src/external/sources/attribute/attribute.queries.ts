import { QuantityDatumType } from "@mimirorg/typelibrary-types";
import { useQuery } from "@tanstack/react-query";
import { attributeApi } from "external/sources/attribute/attribute.api";

const keys = {
  allAttributes: ["attributes"] as const,
  attributeLists: () => [...keys.allAttributes, "list"] as const,
  allPredefined: ["attributesPredefined"] as const,
  predefinedLists: () => [...keys.allPredefined, "list"] as const,
  allQuantityDatum: ["quantityDatum"] as const,
  quantityDatum: (datumType: QuantityDatumType) => [...keys.allQuantityDatum, datumType] as const,
};

export const useGetAttributes = () => useQuery(keys.attributeLists(), attributeApi.getAttributes);

export const useGetAttributesPredefined = () => useQuery(keys.predefinedLists(), attributeApi.getAttributesPredefined);

export const useGetQuantityDatum = (datumType: QuantityDatumType) =>
  useQuery(keys.quantityDatum(datumType), () => attributeApi.getQuantityDatum(datumType));
