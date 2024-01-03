import { Box, Flexbox, Text, Tooltip } from "@mimirorg/component-library";
import AttributeIcon from "components/AttributeIcon";
import BlockIcon from "components/BlockIcon";
import TerminalIcon from "components/TerminalIcon";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface ApprovalCardHeaderProps {
  children?: ReactNode;
  objectType?: string;
}

const ApprovalCardHeader = ({ children, objectType }: ApprovalCardHeaderProps) => {
  function getIcon(type: string) {
    switch (type) {
      case "terminal":
        return <TerminalIcon size={1} />;
      case "block":
        return <BlockIcon size={1} />;
      case "attribute":
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
