import { AspectObjectLibAm, State } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { aspectObjectApi } from "external/sources/aspectobject/aspectObject.api";

const keys = {
  all: ["aspectObjects"] as const,
  lists: () => [...keys.all, "list"] as const,
  aspectObject: (id?: string) => [...keys.lists(), id] as const,
};

export const useGetAspectObjects = () => useQuery(keys.lists(), aspectObjectApi.getAspectObjects);

export const useGetAspectObject = (id?: string) =>
  useQuery(keys.aspectObject(id), () => aspectObjectApi.getAspectObject(id), { enabled: !!id, retry: false });

export const useGetLatestApprovedAspectObject = (id?: string, enable = true) =>
  useQuery(keys.aspectObject("latest-approved/" + id), () => aspectObjectApi.getLatestApprovedAspectObject(id), {
    enabled: enable && !!id,
    retry: false,
  });

export const useCreateAspectObject = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AspectObjectLibAm) => aspectObjectApi.postAspectObject(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateAspectObject = (id?: string) => {
  const queryClient = useQueryClient();

  return useMutation((item: AspectObjectLibAm) => aspectObjectApi.putAspectObject(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.aspectObject(unit.id)),
  });
};

export const usePatchAspectObjectState = () => {
  const queryClient = useQueryClient();

  return useMutation(
    (item: { id: string; state: State }) => aspectObjectApi.patchAspectObjectState(item.id, item.state),
    {
      onSuccess: () => queryClient.invalidateQueries(keys.lists()),
    }
  );
};

export const useDeleteAspectObject = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation(() => aspectObjectApi.deleteAspectObject(id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
