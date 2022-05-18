import { useMutation, useQuery, useQueryClient } from "react-query";
import { AttributeLibAm } from "../../../models/tyle/application/attributeLibAm";
import { apiAttribute } from "../../api/tyle/apiAttribute";
import { Aspect } from "../../../models/tyle/enums/aspect";

const keys = {
  allAttributes: ["attributes"] as const,
  attributeLists: () => [...keys.allAttributes, "list"] as const,
  attributeAspectList: (aspect: Aspect) => [...keys.attributeLists(), { aspect }] as const,
  allPredefined: ["attributesPredefined"] as const,
  predefinedLists: () => [...keys.allPredefined, "list"] as const,
};

export const useGetAttributes = () => useQuery(keys.attributeLists(), apiAttribute.getAttributes);

export const useGetAttributesByAspect = (aspect: Aspect) =>
  useQuery(keys.attributeAspectList(aspect), () => apiAttribute.getAttributesByAspect(aspect));

export const useCreateAttribute = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AttributeLibAm) => apiAttribute.postAttribute(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.attributeLists()),
  });
};

export const useGetAttributesPredefined = () => useQuery(keys.predefinedLists(), apiAttribute.getAttributesPredefined);
