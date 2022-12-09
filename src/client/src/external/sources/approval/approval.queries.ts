import { useQuery } from "@tanstack/react-query";
import { approvalApi } from "external/sources/approval/approval.api";

export const approvalKeys = {
  all: ["approvals"] as const,
  lists: () => [...approvalKeys.all, "list"] as const,
};

export const useGetApprovals = () => useQuery(approvalKeys.lists(), approvalApi.getApprovals);
