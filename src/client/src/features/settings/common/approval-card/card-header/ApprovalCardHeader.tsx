import { Box, Flexbox, Text, Tooltip } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import TerminalIcon from "../../../../icons/TerminalIcon";
import BlockIcon from "../../../../icons/BlockIcon";
import AttributeIcon from "../../../../icons/AttributeIcon";
import RdsIcon from "../../../../icons/RdsIcon";
import UnitIcon from "../../../../icons/UnitIcon";
import { ReactNode } from "react";
import QuantityDatumIcon from "../../../../icons/QuantityDatumIcon";

interface ApprovalCardHeaderProps {
  children?: ReactNode;
  objectType?: string;
}

export const ApprovalCardHeader = ({ children, objectType }: ApprovalCardHeaderProps) => {
  function getIcon(type: string) {
    switch (type) {
      case "Terminal":
        return <TerminalIcon size={1} />;
      case "Block":
        return <BlockIcon size={1} />;
      case "Attribute":
        return <AttributeIcon size={1} />;
      case "Rds":
        return <RdsIcon size={1} />;
      case "Unit":
        return <UnitIcon size={1} />;
      default:
        return <QuantityDatumIcon size={1} />;
    }
  }

  const theme = useTheme();

  return (
    <Box display={"flex"} gap={theme.mimirorg.spacing.l} alignItems={"center"} justifyContent={"space-between"}>
      {children}
      <Flexbox flexFlow={"column"} alignItems={"center"}>
        <Tooltip content={<Text variant={"body-small"}>{objectType}</Text>}>
          <div>{objectType && getIcon(objectType)}</div>
        </Tooltip>
      </Flexbox>
    </Box>
  );
};
