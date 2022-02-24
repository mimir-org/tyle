import { useMutation, useQuery, useQueryClient } from "react-query";
import { PurposeLibAm } from "../../../models/typeLibrary/application/purposeLibAm";
import { apiPurpose } from "../../api/typeLibrary/apiPurpose";

const keys = {
  all: ["purposes"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetPurposes = () => useQuery(keys.lists(), apiPurpose.getPurposes);

export const useCreatePurpose = () => {
  const queryClient = useQueryClient();

  return useMutation((item: PurposeLibAm) => apiPurpose.postPurpose(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
