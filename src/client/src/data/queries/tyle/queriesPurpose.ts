import { useMutation, useQuery, useQueryClient } from "react-query";
import { PurposeLibAm } from "../../../models/tyle/application/purposeLibAm";
import { apiPurpose } from "../../api/tyle/apiPurpose";
import { UpdateEntity } from "../../types/updateEntity";

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

export const useUpdatePurpose = () => {
  const queryClient = useQueryClient();

  return useMutation((item: UpdateEntity<PurposeLibAm>) => apiPurpose.putPurpose(item.id, item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
