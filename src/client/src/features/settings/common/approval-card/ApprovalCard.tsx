import { MotionApprovalCardContainer } from "features/settings/common/approval-card/ApprovalCard.styled";
import { useRef } from "react";
import { useTheme } from "styled-components";
import { ApprovalCardHeader } from "features/settings/common/approval-card/card-header/ApprovalCardHeader";
import { ApprovalCardForm } from "features/settings/common/approval-card/card-form/ApprovalCardForm";
import { Divider, Flexbox, Text } from "@mimirorg/component-library";
import { AttributeView } from "common/types/attributes/attributeView";
import { TerminalView } from "common/types/terminals/terminalView";
import { BlockView } from "common/types/blocks/blockView";

export type ApprovalCardProps = {
  item: AttributeView | TerminalView | BlockView;
  itemType: "attribute" | "terminal" | "block";
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
export const ApprovalCard = ({ item, itemType, selected }: ApprovalCardProps) => {
  const theme = useTheme();
  const cardRef = useRef(null);

  return (
    <MotionApprovalCardContainer
      key={item.id}
      ref={cardRef}
      variant={selected ? "selected" : "filled"}
      layout={"position"}
      {...theme.mimirorg.animation.selectHover}
    >
      <Text variant={"title-medium"}>{itemType}</Text>
      <Divider orientation={"horizontal"} color={"#2e2e2e"} />
      <ApprovalCardHeader objectType={"Attribute"}>
        <Text variant={"title-large"}>{item.name}</Text>
      </ApprovalCardHeader>
      <Flexbox flexFlow={"column"} justifyContent={"space-between"} style={{ height: "100%" }}>
        <Text variant={"title-small"}>{item.description}</Text>
        <Divider orientation={"horizontal"} color={"#2e2e2e"} />
        <ApprovalCardForm item={item} itemType={itemType} />
      </Flexbox>
    </MotionApprovalCardContainer>
  );
};
