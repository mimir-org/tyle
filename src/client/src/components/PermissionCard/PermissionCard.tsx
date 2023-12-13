import { useRef } from "react";
import { useTheme } from "styled-components";
import { UserItem } from "types/userItem";
import { useUserDescriptors } from "./PermissionCard.helpers";
import MotionPermissionCardContainer from "./PermissionCard.styled";
import PermissionCardDetails from "./PermissionCardDetails";
import PermissionCardForm, { PermissionCardFormProps } from "./PermissionCardForm";
import PermissionCardHeader from "./PermissionCardHeader";

export type AccessCardProps = PermissionCardFormProps & {
  user: UserItem;
  selected?: boolean;
  handleRoleChange: (user: UserItem, newRole: string | undefined) => void
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

  const { formId, onSubmit, showSubmitButton, handleRoleChange } = delegated;

  return (
    <MotionPermissionCardContainer
      key={user.id}
      ref={cardRef}
      variant={selected ? "selected" : "filled"}
      layout={"position"}
      {...theme.mimirorg.animation.fade}
    >
      <PermissionCardHeader>{user.name}</PermissionCardHeader>
      <PermissionCardDetails descriptors={userDescriptors} />
      <PermissionCardForm user={user} formId={formId} onSubmit={onSubmit} showSubmitButton={showSubmitButton} handleRoleChange={handleRoleChange} />
    </MotionPermissionCardContainer>
  );
};

export default PermissionCard;
