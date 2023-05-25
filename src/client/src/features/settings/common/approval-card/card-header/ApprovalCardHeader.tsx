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
  children: ReactNode;
  objectType?: string;
}

export const ApprovalCardHeader = ({ children, objectType }: ApprovalCardHeaderProps) => {
  function getIcon(type: string) {
    switch (type) {
      case "Terminal":
        return <TerminalIcon />;
      case "AspectObject":
        return <AspectObjectIcon />;
      case "Attribute":
        return <AttributeIcon />;
      case "Rds":
        return <RdsIcon />;
      case "Unit":
        return <UnitIcon />;
      default:
        return <QuantityDatumIcon />;
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
