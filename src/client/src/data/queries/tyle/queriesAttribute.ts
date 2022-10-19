import { QuantityDatumType } from "@mimirorg/typelibrary-types";
import { useQuery } from "react-query";
import { apiAttribute } from "../../api/tyle/apiAttribute";

const keys = {
  allAttributes: ["attributes"] as const,
  attributeLists: () => [...keys.allAttributes, "list"] as const,
  allPredefined: ["attributesPredefined"] as const,
  predefinedLists: () => [...keys.allPredefined, "list"] as const,
  allQuantityDatum: ["quantityDatum"] as const,
  quantityDatum: (datumType: QuantityDatumType) => [...keys.allQuantityDatum, datumType] as const,
};

export const useGetAttributes = () => useQuery(keys.attributeLists(), apiAttribute.getAttributes);

export const useGetAttributesPredefined = () => useQuery(keys.predefinedLists(), apiAttribute.getAttributesPredefined);

export const useGetQuantityDatum = (datumType: QuantityDatumType) =>
  useQuery(keys.quantityDatum(datumType), () => apiAttribute.getQuantityDatum(datumType));
