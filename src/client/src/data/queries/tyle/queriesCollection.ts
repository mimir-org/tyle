import { useMutation, useQuery, useQueryClient } from "react-query";
import { CollectionLibAm } from "../../../models/tyle/application/collectionLibAm";
import { apiCollection } from "../../api/tyle/apiCollection";
import { UpdateEntity } from "../../types/updateEntity";

const keys = {
  all: ["collections"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetCollections = () => useQuery(keys.lists(), apiCollection.getCollections);

export const useCreateCollection = () => {
  const queryClient = useQueryClient();

  return useMutation((item: CollectionLibAm) => apiCollection.postCollection(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};

export const useUpdateCollection = () => {
  const queryClient = useQueryClient();

  return useMutation((item: UpdateEntity<CollectionLibAm>) => apiCollection.putCollection(item.id, item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
