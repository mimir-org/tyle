import Divider from "components/Divider";
import Flexbox from "components/Flexbox";
import Text from "components/Text";
import { useRef } from "react";
import { useTheme } from "styled-components";
import { AttributeView } from "types/attributes/attributeView";
import { BlockView } from "types/blocks/blockView";
import { TerminalView } from "types/terminals/terminalView";
import MotionApprovalCardContainer from "./ApprovalCard.styled";
import ApprovalCardForm from "./ApprovalCardForm";
import ApprovalCardHeader from "./ApprovalCardHeader";

export type ApprovalCardProps = {
  item: AttributeView | TerminalView | BlockView;
  itemType: "attribute" | "terminal" | "block";
  selected?: string;
  disabledButton: boolean;
};

const ApprovalCard = ({ item, itemType, selected, disabledButton }: ApprovalCardProps) => {
  const theme = useTheme();
  const cardRef = useRef(null);

  return (
    <MotionApprovalCardContainer
      key={item.id}
      ref={cardRef}
      variant={selected ? "selected" : "filled"}
      layout={"position"}
      {...theme.tyle.animation.selectHover}
    >
      <Text variant={"title-medium"}>{itemType}</Text>
      <Divider orientation={"horizontal"} color={"#2e2e2e"} />
      <ApprovalCardHeader objectType={itemType}>
        <Text variant={"title-large"}>{item.name}</Text>
      </ApprovalCardHeader>
      <Flexbox flexFlow={"column"} justifyContent={"space-between"} style={{ height: "100%" }}>
        <Text variant={"title-small"}>{item.description}</Text>
        <Divider orientation={"horizontal"} color={"#2e2e2e"} />
        <ApprovalCardForm item={item} itemType={itemType} disabledButton={disabledButton} />
      </Flexbox>
    </MotionApprovalCardContainer>
  );
};

export default ApprovalCard;
