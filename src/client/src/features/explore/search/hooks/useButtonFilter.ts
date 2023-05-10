import { StateItem } from "common/types/stateItem";
import { UserItem } from "common/types/userItem";
import { useEffect, useState } from "react";
import { MimirorgPermission, State } from "@mimirorg/typelibrary-types";
import { isAspectObjectItem } from "../guards";

export interface ButtonState {
  clone: boolean;
  edit: boolean;
  delete: boolean;
  approve: boolean;
  deleted: boolean;
  approved: boolean;
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
    approve: false,
    deleted: false,
    approved: false,
  };

  const [buttonState, setButtonState] = useState<ButtonState>(initialState);

  useEffect(() => {
    const currentButtonState: ButtonState = {
      clone: allowClone(item ?? null, user),
      edit: allowEdit(item ?? null, user),
      delete: allowDelete(item ?? null, user),
      approve: allowApprove(item ?? null, user),
      deleted: item?.state === State.Deleted,
      approved: item?.state === State.Approved,
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

const allowEdit = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  let permissionForCompany: MimirorgPermission;
  if (isAspectObjectItem(item)) {
    permissionForCompany = user.permissions[item.companyId]?.value;
  } else {
    permissionForCompany = user.permissions[0].value;
  }
  if (permissionForCompany == null) return false;

  const hasMinimumWrite = (permissionForCompany & MimirorgPermission.Write) === MimirorgPermission.Write;
  return hasMinimumWrite && item.state !== State.Approve && item.state !== State.Delete && item.state !== State.Deleted;
};

const allowDelete = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  let permissionForCompany: MimirorgPermission;
  if (isAspectObjectItem(item)) {
    permissionForCompany = user.permissions[item.companyId]?.value;
  } else {
    permissionForCompany = user.permissions[0].value;
  }
  if (permissionForCompany == null) return false;

  const hasMinimumWrite = (permissionForCompany & MimirorgPermission.Write) === MimirorgPermission.Write;
  return hasMinimumWrite && item.state === State.Draft;
};

const allowApprove = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  let permissionForCompany: MimirorgPermission;
  if (isAspectObjectItem(item)) {
    permissionForCompany = user.permissions[item.companyId]?.value;
  } else {
    permissionForCompany = user.permissions[0].value;
  }
  if (permissionForCompany == null) return false;

  const hasMinimumWrite = (permissionForCompany & MimirorgPermission.Write) === MimirorgPermission.Write;
  return hasMinimumWrite && item.state === State.Draft;
};
