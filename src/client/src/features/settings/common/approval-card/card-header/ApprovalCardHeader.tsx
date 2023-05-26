import { Box, Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { useTheme } from "styled-components";
import TerminalIcon from "../../../../icons/TerminalIcon";
import AspectObjectIcon from "../../../../icons/AspectObjectIcon";
import AttributeIcon from "../../../../icons/AttributeIcon";
import RdsIcon from "../../../../icons/RdsIcon";
import UnitIcon from "../../../../icons/UnitIcon";
import { ReactNode } from "react";
import { Tooltip } from "../../../../../complib/data-display";
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
      case "AspectObject":
        return <AspectObjectIcon size={1} />;
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
    <Box display={"flex"} gap={theme.tyle.spacing.l} alignItems={"center"} justifyContent={"space-between"}>
      {children}
      <Flexbox flexFlow={"column"} alignItems={"center"}>
        <Tooltip content={<Text variant={"body-small"}>{objectType}</Text>}>
          <div>{objectType && getIcon(objectType)}</div>
        </Tooltip>
      </Flexbox>
    </Box>
  );
};
