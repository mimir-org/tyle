import { MimirorgPermission, State } from "@mimirorg/typelibrary-types";
import { hasWriteAccess } from "hooks/useHasWriteAccess";
import { useEffect, useState } from "react";
import { ItemType } from "types/itemTypes";
import { StateItem } from "types/stateItem";
import { UserItem } from "types/userItem";
import { isBlockItem } from "./isItemValidators";

export interface ButtonState {
  clone: boolean;
  edit: boolean;
  delete: boolean;
  review: boolean;
  approved: boolean;
}

/**
 * Hook for find search item filter buttons
 * @param item state item
 * @param user current user
 */
export const useButtonStateFilter = (item: ItemType | null, user: UserItem | null) => {
  const initialState: ButtonState = {
    clone: false,
    edit: false,
    delete: false,
    review: false,
    approved: false,
  };

  const [buttonState, setButtonState] = useState<ButtonState>(initialState);

  useEffect(() => {
    const currentButtonState: ButtonState = {
      clone: allowClone(item ?? null, user),
      edit: allowEdit(item ?? null, user),
      delete: allowDelete(item ?? null, user),
      review: allowStateChange(item ?? null, user),
      approved: item?.state === State.Approved,
    };

    setButtonState(currentButtonState);
  }, [item, user]);

  return buttonState;
};

const allowClone = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  return hasWriteAccess(user);
};

const allowEdit = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  let permissionForCompany: MimirorgPermission;
  if (isBlockItem(item)) {
    //permissionForCompany = user.permissions[item.companyId]?.value;
    permissionForCompany = user.permissions[0].value;
  } else {
    permissionForCompany = user.permissions[0].value;
  }
  if (permissionForCompany == null) return false;

  return hasWriteAccess(user) && item.state !== State.Review;
};

const allowDelete = (item: ItemType | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  if (item.state === State.Approved) return false;

  if (item.createdBy === user.id && hasWriteAccess(user)) return true;

  let permissionForCompany: MimirorgPermission;
  if (isBlockItem(item)) {
    //permissionForCompany = user.permissions[item.companyId]?.value;
    permissionForCompany = user.permissions[0].value;
  } else {
    permissionForCompany = user.permissions[0].value;
  }
  if (permissionForCompany == null) return false;

  return (permissionForCompany & MimirorgPermission.Approve) === MimirorgPermission.Approve;
};

const allowStateChange = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  let permissionForCompany: MimirorgPermission;
  if (isBlockItem(item)) {
    //permissionForCompany = user.permissions[item.companyId]?.value;
    permissionForCompany = user.permissions[0].value;
  } else {
    permissionForCompany = user.permissions[0].value;
  }
  if (permissionForCompany == null) return false;

  const hasMinimumWrite = (permissionForCompany & MimirorgPermission.Write) === MimirorgPermission.Write;
  return hasMinimumWrite && item.state === State.Draft;
};
