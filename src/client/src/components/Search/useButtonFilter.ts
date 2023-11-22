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
      review: allowRequestReview(item ?? null, user),
      approved: item?.state === State.Approved,
    };

    setButtonState(currentButtonState);
  }, [item, user]);

  return buttonState;
};

const allowClone = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  if (user.roles.filter((x) => x === "Administrator" || x === "Reviewer" || x === "Contributor").length === 0) {
    return false;
  }

  return true;
};

const allowEdit = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  if (item.state !== State.Draft) return false;

  if (user.roles.filter((x) => x === "Administrator" || x === "Reviewer" || x === "Contributor").length === 0) {
    return false;
  }

  return true;
};

const allowDelete = (item: ItemType | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  if (item.state === State.Approved) return false;

  if(item.createdBy === user.id)
  return true;

  if (user.roles.filter((x) => x === "Administrator" || x === "Reviewer").length === 0) {
    return false;
  }

  return true;
};

const allowRequestReview = (item: StateItem | null, user: UserItem | null): boolean => {
  if (item == null || user == null) return false;

  if (item.state !== State.Draft) return false;

  if (user.roles.filter((x) => x === "Administrator" || x === "Reviewer" || x === "Contributor").length === 0) {
    return false;
  }

  return true;
};
