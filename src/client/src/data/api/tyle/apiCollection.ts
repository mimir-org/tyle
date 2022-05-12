import { apiClient } from "../apiClient";
import { CollectionLibCm } from "../../../models/tyle/client/collectionLibCm";
import { CollectionLibAm } from "../../../models/tyle/application/collectionLibAm";

const _basePath = "librarycollection";

export const apiCollection = {
  getCollections() {
    return apiClient.get<CollectionLibCm[]>(_basePath).then((r) => r.data);
  },
  putCollection(id: string, item: CollectionLibAm) {
    return apiClient.put<CollectionLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postCollection(item: CollectionLibAm) {
    return apiClient.post<CollectionLibCm>(_basePath, item).then((r) => r.data);
  },
};
