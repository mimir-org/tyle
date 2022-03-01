import { apiClient } from "../apiClient";
import { PurposeLibCm } from "../../../models/typeLibrary/client/purposeLibCm";
import { PurposeLibAm } from "../../../models/typeLibrary/application/purposeLibAm";

const _basePath = "librarypurpose";

export const apiPurpose = {
  getPurposes() {
    return apiClient.get<PurposeLibCm[]>(_basePath).then((r) => r.data);
  },
  putPurpose(id: string, item: PurposeLibAm) {
    return apiClient.put<PurposeLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  postPurpose(item: PurposeLibAm) {
    return apiClient.post<PurposeLibCm>(_basePath, item).then((r) => r.data);
  },
};
