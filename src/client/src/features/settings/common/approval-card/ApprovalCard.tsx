import { useApprovalDescriptors } from "features/settings/common/approval-card/ApprovalCard.helpers";
import { MotionApprovalCardContainer } from "features/settings/common/approval-card/ApprovalCard.styled";
import { useRef } from "react";
import { useTheme } from "styled-components";
import { ApprovalCardHeader } from "features/settings/common/approval-card/card-header/ApprovalCardHeader";
import { ApprovalCardDetails } from "features/settings/common/approval-card/card-details/ApprovalCardDetails";
import {
  ApprovalCardForm,
  ApprovalCardFormProps,
} from "features/settings/common/approval-card/card-form/ApprovalCardForm";
import { ApprovalCm } from "@mimirorg/typelibrary-types";

export type ApprovalCardProps = ApprovalCardFormProps & {
  item: ApprovalCm;
  selected?: string;
};

/**
 * Shows a card with information about the user and a form for altering the user's permission level *
 * @param item
 * @param selected property for overriding the cards selected visual state
 * @param delegated receives all the properties of PermissionCardFormProps
 * @see PermissionCardFormProps
 * @constructor
 */
export const ApprovalCard = ({ item, selected, ...delegated }: ApprovalCardProps) => {
  const theme = useTheme();
  const cardRef = useRef(null);
  const approvalDescriptors = useApprovalDescriptors(item);
  const { formId, onSubmit, onReject, showSubmitButton } = delegated;

  if (item == null) return <></>;

  return (
    <MotionApprovalCardContainer
      key={item.id}
      ref={cardRef}
      variant={selected ? "selected" : "filled"}
      layout={"position"}
      {...theme.tyle.animation.selectHover}
    >
      <ApprovalCardHeader>{item.name}</ApprovalCardHeader>
      <ApprovalCardDetails descriptors={approvalDescriptors} />
      <ApprovalCardForm
        item={item}
        formId={formId}
        onSubmit={onSubmit}
        onReject={onReject}
        showSubmitButton={showSubmitButton}
      />
    </MotionApprovalCardContainer>
  );
};
