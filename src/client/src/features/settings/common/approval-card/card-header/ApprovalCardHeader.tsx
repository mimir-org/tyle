import { Box, Flexbox, Text, Tooltip } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import TerminalIcon from "../../../../../components/TerminalIcon/TerminalIcon";
import BlockIcon from "../../../../../components/BlockIcon/BlockIcon";
import AttributeIcon from "../../../../../components/AttributeIcon/AttributeIcon";
import { ReactNode } from "react";

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
