import { State } from "@mimirorg/typelibrary-types";
import { useEffect, useState } from "react";
import { ItemType } from "types/itemTypes";
import { StateItem } from "types/stateItem";
import { UserItem } from "types/userItem";

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

  return true; //hasWriteAccess(user);
};

const allowEdit = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  return true; //hasWriteAccess(user) && item.state !== State.Review;
};

const allowDelete = (item: ItemType | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  if (item.state === State.Approved) return false;

  if (item.createdBy === user.id) return true; // && hasWriteAccess(user)) return true;

  return true;
};

const allowStateChange = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  return true;
};
