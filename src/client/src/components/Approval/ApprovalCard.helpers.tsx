import { ApprovalCm, State } from "@mimirorg/typelibrary-types";

export const useApprovalDescriptors = (approval: ApprovalCm): { [key: string]: string } => {
  const descriptors: { [key: string]: string } = {};

  if (approval.companyName) {
    descriptors["Company"] = approval.companyName;
  }

  if (approval.objectType) {
    descriptors["Type"] = approval.objectType;
  }

  if (approval.userName) {
    descriptors["User"] = approval.userName;
  }

  if (approval.stateName) {
    descriptors["State"] = approval.stateName;
  }

  return descriptors;
};

export const approvalFilter = (approval: ApprovalCm): boolean => {
  if (approval == null) return false;

  return approval.state === State.Review;
};
