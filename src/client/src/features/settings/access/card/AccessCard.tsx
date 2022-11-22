import { MimirorgUserCm } from "@mimirorg/typelibrary-types";
import { useUserDescriptors } from "features/settings/access/card/AccessCard.helpers";
import { MotionAccessCardContainer } from "features/settings/access/card/AccessCard.styled";
import { AccessCardDetails } from "features/settings/access/card/details/AccessCardDetails";
import { AccessCardForm } from "features/settings/access/card/form/AccessCardForm";
import { AccessCardHeader } from "features/settings/access/card/header/AccessCardHeader";
import { useRef } from "react";
import { useTheme } from "styled-components";
import { useHover } from "usehooks-ts";

export interface AccessCardProps {
  user: MimirorgUserCm;
}

export const AccessCard = ({ user }: AccessCardProps) => {
  const theme = useTheme();
  const cardRef = useRef(null);
  const isHovered = useHover(cardRef);
  const userDescriptors = useUserDescriptors(user);

  return (
    <MotionAccessCardContainer
      ref={cardRef}
      variant={isHovered ? "selected" : "filled"}
      layout={"position"}
      {...theme.tyle.animation.fade}
    >
      <AccessCardHeader>{`${user.firstName} ${user.lastName}`}</AccessCardHeader>
      <AccessCardDetails descriptors={userDescriptors} />
      <AccessCardForm user={user} />
    </MotionAccessCardContainer>
  );
};
