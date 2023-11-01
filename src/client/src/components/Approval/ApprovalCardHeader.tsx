import { Box, Flexbox, Text, Tooltip } from "@mimirorg/component-library";
import { ReactNode } from "react";
import { useTheme } from "styled-components";
import AttributeIcon from "../AttributeIcon";
import BlockIcon from "../BlockIcon";
import TerminalIcon from "../TerminalIcon";

interface ApprovalCardHeaderProps {
  children?: ReactNode;
  objectType?: string;
}

const ApprovalCardHeader = ({ children, objectType }: ApprovalCardHeaderProps) => {
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

export default ApprovalCardHeader;
