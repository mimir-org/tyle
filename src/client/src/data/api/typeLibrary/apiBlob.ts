import { apiClient } from "../apiClient";
import { BlobLibCm } from "../../../models/typeLibrary/client/blobLibCm";
import { BlobLibAm } from "../../../models/typeLibrary/application/blobLibAm";

const _basePath = "libraryblob";

export const apiBlob = {
  getBlobs() {
    return apiClient.get<BlobLibCm[]>(_basePath).then((r) => r.data);
  },
  postBlob(item: BlobLibAm) {
    return apiClient.post<BlobLibCm>(_basePath, item).then((r) => r.data);
  },
};
