import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { mediumApi } from "./medium.api";
import { RdlMediumRequest } from "common/types/terminals/rdlMediumRequest";

const keys = {
  allMedia: ["media"] as const,
  mediumLists: () => [...keys.allMedia, "list"] as const,
  medium: (id: number) => [...keys.mediumLists(), id] as const,
};

export const useGetMedia = () => useQuery(keys.mediumLists(), mediumApi.getMedia);

export const useGetMedium = (id: number) =>
  useQuery(keys.medium(id), () => mediumApi.getMedium(id), { enabled: !!id, retry: false });

export const useCreateMedium = () => {
  const queryClient = useQueryClient();

  return useMutation((item: RdlMediumRequest) => mediumApi.postMedium(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.allMedia),
  });
};

export const useDeleteMedium = (id: number) => {
  const queryClient = useQueryClient();

  return useMutation(() => mediumApi.deleteMedium(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.mediumLists()),
  });
};
