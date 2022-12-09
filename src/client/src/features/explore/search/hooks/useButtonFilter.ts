import { StateItem } from "common/types/stateItem";
import { UserItem } from "common/types/userItem";
import { useEffect, useState } from "react";
import { MimirorgPermission, State } from "@mimirorg/typelibrary-types";

export interface ButtonState {
  clone: boolean;
  edit: boolean;
  delete: boolean;
  approveCompany: boolean;
  approveGlobal: boolean;
  deleted: boolean;
  approvedComapny: boolean;
  approvedGlobal: boolean;
}

/**
 * Hook for find search item filter buttons
 * @param item state item
 * @param user current user
 */
export const useButtonStateFilter = (item: StateItem | null, user: UserItem | null) => {
  const initialState: ButtonState = {
    clone: false,
    edit: false,
    delete: false,
    approveCompany: false,
    approveGlobal: false,
    deleted: false,
    approvedComapny: false,
    approvedGlobal: false,
  };

  const [buttonState, setButtonState] = useState<ButtonState>(initialState);

  useEffect(() => {
    const currentButtonState: ButtonState = {
      clone: allowClone(item ?? null, user),
      edit: allowEditDelete(item ?? null, user),
      delete: allowEditDelete(item ?? null, user),
      approveCompany: allowApproveCompany(item ?? null, user),
      approveGlobal: allowApproveGlobal(item ?? null, user),
      deleted: item?.state === State.Deleted,
      approvedComapny: item?.state === State.ApprovedCompany,
      approvedGlobal: item?.state === State.ApprovedGlobal,
    };

    setButtonState(currentButtonState);
  }, [item, user]);

  return buttonState;
};

const allowClone = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  const anyWrite = Object.values(user.permissions).some(
    (x) => (x.value & MimirorgPermission.Write) === MimirorgPermission.Write
  );
  return anyWrite && item.state !== State.Delete && item.state !== State.Deleted;
};

const allowEditDelete = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  const permissionForCompany = user.permissions[item.companyId]?.value;
  if (permissionForCompany == null) return false;

  const hasMinimumWrite = (permissionForCompany & MimirorgPermission.Write) === MimirorgPermission.Write;
  return hasMinimumWrite && item.state !== State.Delete && item.state !== State.Deleted;
};

const allowApproveCompany = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  const permissionForCompany = user.permissions[item.companyId]?.value;
  if (permissionForCompany == null) return false;

  const hasMinimumWrite = (permissionForCompany & MimirorgPermission.Write) === MimirorgPermission.Write;
  return (
    hasMinimumWrite &&
    item.state !== State.Delete &&
    item.state !== State.Deleted &&
    item.state !== State.ApproveCompany &&
    item.state !== State.ApprovedCompany
  );
};

const allowApproveGlobal = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  const permissionForCompany = user.permissions[item.companyId]?.value;
  if (permissionForCompany == null) return false;

  const anyWrite = Object.values(user.permissions).some(
    (x) => (x.value & MimirorgPermission.Write) === MimirorgPermission.Write
  );
  return (
    anyWrite &&
    item.state !== State.Delete &&
    item.state !== State.Deleted &&
    item.state !== State.ApproveGlobal &&
    item.state !== State.ApprovedGlobal
  );
};
