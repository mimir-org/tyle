import { UserItem } from "common/types/userItem";
import { PermissionCardDetails } from "components/PermissionCard/PermissionCardDetails";
import { PermissionCardForm, PermissionCardFormProps } from "components/PermissionCard/PermissionCardForm";
import { PermissionCardHeader } from "./PermissionCardHeader";
import { useUserDescriptors } from "components/PermissionCard/PermissionCard.helpers";
import { MotionPermissionCardContainer } from "components/PermissionCard/PermissionCard.styled";
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
const PermissionCard = ({ user, selected, ...delegated }: AccessCardProps) => {
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

export default PermissionCard;