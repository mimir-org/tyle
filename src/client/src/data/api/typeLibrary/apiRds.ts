import { apiClient } from "../apiClient";
import { RdsLibCm } from "../../../models/typeLibrary/client/rdsLibCm";
import { RdsCategoryLibAm } from "../../../models/typeLibrary/application/rdsCategoryLibAm";
import { RdsCategoryLibCm } from "../../../models/typeLibrary/client/rdsCategoryLibCm";
import { Aspect } from "../../../models/typeLibrary/enums/aspect";

const _basePath = "libraryrds";

export const apiRds = {
  getRds() {
    return apiClient.get<RdsLibCm[]>(_basePath).then((r) => r.data);
  },
  getRdsByAspect(aspect: Aspect) {
    return apiClient.get<RdsLibCm[]>(`${_basePath}/${aspect}`).then((r) => r.data);
  },
  getRdsCategories() {
    return apiClient.get<RdsCategoryLibCm[]>(`${_basePath}/category`).then((r) => r.data);
  },
  putRdsCategory(id: string, item: RdsCategoryLibAm) {
    return apiClient.put<RdsCategoryLibCm>(`${_basePath}/category/${id}`, item).then((r) => r.data);
  },
  postRdsCategory(item: RdsCategoryLibAm) {
    return apiClient.post<RdsCategoryLibCm>(`${_basePath}/category`, item).then((r) => r.data);
  },
};
