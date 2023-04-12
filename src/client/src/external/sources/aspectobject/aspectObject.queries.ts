import { AspectObjectLibAm, State } from "@mimirorg/typelibrary-types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { aspectObjectApi } from "external/sources/aspectobject/aspectObject.api";

const keys = {
  all: ["aspectObjects"] as const,
  lists: () => [...keys.all, "list"] as const,
  aspectObject: (id?: number) => [...keys.lists(), id] as const,
};

export const useGetAspectObjects = () => useQuery(keys.lists(), aspectObjectApi.getLibraryAspectObjects);

export const useGetAspectObject = (id?: number) =>
  useQuery(keys.aspectObject(id), () => aspectObjectApi.getLibraryAspectObject(id), { enabled: !!id, retry: false });

export const useCreateAspectObject = () => {
  const queryClient = useQueryClient();

  return useMutation((item: AspectObjectLibAm) => aspectObjectApi.postLibraryAspectObject(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateAspectObject = (id?: number) => {
  const queryClient = useQueryClient();

  return useMutation((item: AspectObjectLibAm) => aspectObjectApi.putLibraryAspectObject(item, id), {
    onSuccess: (unit) => queryClient.invalidateQueries(keys.aspectObject(unit.id)),
  });
};

export const usePatchNAspectObjectState = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: number; state: State }) => aspectObjectApi.patchLibraryAspectObjectState(item.id, item.state), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const usePatchAspectObjectStateReject = () => {
  const queryClient = useQueryClient();

  return useMutation((item: { id: number }) => aspectObjectApi.patchLibraryAspectObjectStateReject(item.id), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
