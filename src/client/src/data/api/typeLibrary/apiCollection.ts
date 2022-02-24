import { apiClient } from "../apiClient";
import { CollectionLibCm } from "../../../models/typeLibrary/client/collectionLibCm";
import { CollectionLibAm } from "../../../models/typeLibrary/application/collectionLibAm";

const _basePath = "librarycollection";

export const apiCollection = {
  getCollections() {
    return apiClient.get<CollectionLibCm[]>(_basePath).then((r) => r.data);
  },
  postCollection(item: CollectionLibAm) {
    return apiClient.post<CollectionLibCm>(_basePath, item).then((r) => r.data);
  },
};
