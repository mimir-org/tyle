import { MotionBox } from "@mimirorg/component-library";
import { useTheme } from "styled-components";
import { TerminalItem } from "types/terminalItem";
import PreviewPanel from "./PreviewPanel";

/**
 * Component that displays information about a given terminal.
 *
 * @param props receives all properties of a TerminalItem
 * @constructor
 */
export const TerminalPanel = ({ name, description, color, attributes, tokens, kind }: TerminalItem) => {
  const theme = useTheme();

  return (
    <MotionBox
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.mimirorg.spacing.xxxl}
      maxHeight={"100%"}
      overflow={"hidden"}
      {...theme.mimirorg.animation.fade}
    >
      <PreviewPanel
        name={name}
        description={description}
        tokens={tokens}
        attributes={attributes}
        kind={kind}
        state={""}
      />
    </MotionBox>
  );
};
