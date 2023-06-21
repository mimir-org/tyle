import { AspectObjectLibAm, AspectObjectLibCm, State, ApprovalDataCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryaspectobject";

export const aspectObjectApi = {
  getAspectObjects() {
    return apiClient.get<AspectObjectLibCm[]>(_basePath).then((r) => r.data);
  },
  getAspectObject(id?: string) {
    return apiClient.get<AspectObjectLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  getLatestApprovedAspectObject(id?: string) {
    return apiClient.get<AspectObjectLibCm>(`${_basePath}/latest-approved/${id}`).then((r) => r.data);
  },
  postAspectObject(item: AspectObjectLibAm) {
    return apiClient.post<AspectObjectLibCm>(_basePath, item).then((r) => r.data);
  },
  putAspectObject(item: AspectObjectLibAm, id?: string) {
    return apiClient.put<AspectObjectLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchAspectObjectState(id: string, state: State) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  deleteAspectObject(id: string) {
    return apiClient.delete(`${_basePath}/${id}`);
  },
};
