import { AspectObjectLibAm, AspectObjectLibCm, State, ApprovalDataCm } from "@mimirorg/typelibrary-types";
import { apiClient } from "external/client/apiClient";

const _basePath = "libraryaspectobject";

export const aspectObjectApi = {
  getLibraryAspectObjects() {
    return apiClient.get<AspectObjectLibCm[]>(_basePath).then((r) => r.data);
  },
  getLibraryAspectObject(id?: number) {
    return apiClient.get<AspectObjectLibCm>(`${_basePath}/${id}`).then((r) => r.data);
  },
  postLibraryAspectObject(item: AspectObjectLibAm) {
    return apiClient.post<AspectObjectLibCm>(_basePath, item).then((r) => r.data);
  },
  putLibraryAspectObject(item: AspectObjectLibAm, id?: number) {
    return apiClient.put<AspectObjectLibCm>(`${_basePath}/${id}`, item).then((r) => r.data);
  },
  patchLibraryAspectObjectState(id: number, state: State) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/${state}`).then((r) => r.data);
  },
  patchLibraryAspectObjectStateReject(id: number) {
    return apiClient.patch<ApprovalDataCm>(`${_basePath}/${id}/state/reject`).then((r) => r.data);
  },
};
