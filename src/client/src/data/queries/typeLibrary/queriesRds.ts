import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiRds } from "../../api/typeLibrary/apiRds";
import { RdsCategoryLibAm } from "../../../models/typeLibrary/application/rdsCategoryLibAm";
import { Aspect } from "../../../models/typeLibrary/enums/aspect";

const keys = {
  allRds: ["rds"] as const,
  rdsLists: () => [...keys.allRds, "list"] as const,
  rdsAspectList: (aspect: Aspect) => [...keys.rdsLists(), { aspect }] as const,
  allRdsCategories: ["rdsCategories"] as const,
  rdsCategoryLists: () => [...keys.allRdsCategories, "list"] as const,
};

export const useGetRds = () => useQuery(keys.rdsLists(), apiRds.getRds);

export const useGetRdsByAspect = (aspect: Aspect) =>
  useQuery(keys.rdsAspectList(aspect), () => apiRds.getRdsByAspect(aspect));

export const useGetRdsCategories = () => useQuery(keys.rdsCategoryLists(), apiRds.getRdsCategories);

export const useCreateRdsCategory = () => {
  const queryClient = useQueryClient();

  return useMutation((item: RdsCategoryLibAm) => apiRds.postRdsCategory(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.rdsCategoryLists()),
  });
};
