import { useMutation, useQuery, useQueryClient } from "react-query";
import { apiBlob } from "../../api/typeLibrary/apiBlob";
import { BlobLibAm } from "../../../models/typeLibrary/application/blobLibAm";

const keys = {
  all: ["blobs"] as const,
  lists: () => [...keys.all, "list"] as const,
};

export const useGetBlobs = () => useQuery(keys.lists(), apiBlob.getBlobs);

export const useCreateBlob = () => {
  const queryClient = useQueryClient();

  return useMutation((item: BlobLibAm) => apiBlob.postBlob(item), {
    onSuccess: () => queryClient.invalidateQueries(keys.lists()),
  });
};
