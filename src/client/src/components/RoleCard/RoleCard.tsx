import { useRef } from "react";
import { useTheme } from "styled-components";
import { UserItem } from "types/userItem";
import { useUserDescriptors } from "./RoleCard.helpers";
import MotionRoleCardContainer from "./RoleCard.styled";
import RoleCardDetails from "./RoleCardDetails";
import RoleCardForm, { RoleCardFormProps } from "./RoleCardForm";
import RoleCardHeader from "./RoleCardHeader";

export type AccessCardProps = RoleCardFormProps & {
  user: UserItem;
  selected?: boolean;
  selectedRole: string;
  setSelectedRole: (role: string) => void;
};

/**
 * Shows a card with information about the user and a form for altering the user's permission level
 *
 * @param user
 * @param selected property for overriding the cards selected visual state
 * @param selectedRole currently selected role in form
 * @param setSelectedRole function to set currently selected role
 * @param delegated receives all the properties of PermissionCardFormProps
 * @see RoleCardFormProps
 * @constructor
 */
const RoleCard = ({ user, selected, selectedRole, setSelectedRole, ...delegated }: AccessCardProps) => {
  const theme = useTheme();
  const cardRef = useRef(null);
  const userDescriptors = useUserDescriptors(user);

  const { formId, showSubmitButton } = delegated;

  return (
    <MotionRoleCardContainer
      key={user.id}
      ref={cardRef}
      variant={selected ? "selected" : "filled"}
      layout={"position"}
      {...theme.tyle.animation.fade}
    >
      <RoleCardHeader>{user.name}</RoleCardHeader>
      <RoleCardDetails descriptors={userDescriptors} />
      <RoleCardForm
        user={user}
        formId={formId}
        showSubmitButton={showSubmitButton}
        selectedRole={selectedRole}
        setSelectedRole={setSelectedRole}
      />
    </MotionRoleCardContainer>
  );
};

export default RoleCard;
