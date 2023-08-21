import { UserItem } from "common/types/userItem";
import { PermissionCardDetails } from "features/settings/common/permission-card/card-details/PermissionCardDetails";
import {
  PermissionCardForm,
  PermissionCardFormProps,
} from "features/settings/common/permission-card/card-form/PermissionCardForm";
import { PermissionCardHeader } from "features/settings/common/permission-card/card-header/PermissionCardHeader";
import { useUserDescriptors } from "features/settings/common/permission-card/PermissionCard.helpers";
import { MotionPermissionCardContainer } from "features/settings/common/permission-card/PermissionCard.styled";
import { useRef } from "react";
import { useTheme } from "styled-components";

export type AccessCardProps = PermissionCardFormProps & {
  user: UserItem;
  selected?: boolean;
};

/**
 * Shows a card with information about the user and a form for altering the user's permission level
 *
 * @param user
 * @param selected property for overriding the cards selected visual state
 * @param delegated receives all the properties of PermissionCardFormProps
 * @see PermissionCardFormProps
 * @constructor
 */
export const PermissionCard = ({ user, selected, ...delegated }: AccessCardProps) => {
  const theme = useTheme();
  const cardRef = useRef(null);
  const userDescriptors = useUserDescriptors(user);

  const { formId, onSubmit, showSubmitButton } = delegated;

  return (
    <MotionPermissionCardContainer
      key={user.id}
      ref={cardRef}
      variant={selected ? "selected" : "filled"}
      layout={"position"}
      {...theme.mimirorg.animation.selectHover}
    >
      <PermissionCardHeader>{user.name}</PermissionCardHeader>
      <PermissionCardDetails descriptors={userDescriptors} />
      <PermissionCardForm user={user} formId={formId} onSubmit={onSubmit} showSubmitButton={showSubmitButton} />
    </MotionPermissionCardContainer>
  );
};
