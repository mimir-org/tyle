import { useMutation, useQuery, useQueryClient } from "react-query";
import { CollectionLibAm } from "../../../models/typeLibrary/application/collectionLibAm";
import { apiCollection } from "../../api/typeLibrary/apiCollection";

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
