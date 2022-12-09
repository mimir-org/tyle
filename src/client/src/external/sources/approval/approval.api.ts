import { ApprovalCm } from "common/types/approvalCm";
import { apiClient } from "external/client/apiClient";

const _basePath = "librarylog";

export const approvalApi = {
  getApprovals() {
    return apiClient.get<ApprovalCm[]>(`${_basePath}/approvals`).then((r) => r.data);
  },
};
