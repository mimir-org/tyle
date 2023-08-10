import { MotionApprovalCardContainer } from "features/settings/common/approval-card/ApprovalCard.styled";
import { useRef } from "react";
import { useTheme } from "styled-components";
import { ApprovalCardHeader } from "features/settings/common/approval-card/card-header/ApprovalCardHeader";
import {
  ApprovalCardForm,
  ApprovalCardFormProps,
} from "features/settings/common/approval-card/card-form/ApprovalCardForm";
import { ApprovalCm } from "@mimirorg/typelibrary-types";
import { Horizontal } from "../../../../complib/data-display/divider/Divider.stories";
import { Flexbox, Text } from "@mimirorg/component-library";

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
  const { formId, onSubmit, onReject, showSubmitButton } = delegated;

  if (item == null) return <></>;

  return (
    <MotionApprovalCardContainer
      key={item.id + item.objectType}
      ref={cardRef}
      variant={selected ? "selected" : "filled"}
      layout={"position"}
      {...theme.tyle.animation.selectHover}
    >
      <Text variant={"title-medium"}>{item.objectType}</Text>
      <Horizontal color={"#2e2e2e"} />
      <ApprovalCardHeader objectType={item.objectType}>
        <Text variant={"title-large"}>{item.name}</Text>
      </ApprovalCardHeader>
      <Flexbox flexFlow={"column"} justifyContent={"space-between"} style={{ height: "100%" }}>
        <Text variant={"title-small"}>{item.description}</Text>
        <Horizontal color={"#2e2e2e"} />
        <ApprovalCardForm
          item={item}
          formId={formId}
          onSubmit={onSubmit}
          onReject={onReject}
          showSubmitButton={showSubmitButton}
        />
      </Flexbox>
    </MotionApprovalCardContainer>
  );
};
