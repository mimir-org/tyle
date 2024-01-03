import { MotionBox } from "components/Box";
import { useTheme } from "styled-components";
import { TerminalItem } from "types/terminalItem";
import PreviewPanel from "./PreviewPanel";

/**
 * Component that displays information about a given terminal.
 *
 * @param props receives all properties of a TerminalItem
 * @constructor
 */
export const TerminalPanel = ({ name, description, attributes, tokens, kind }: TerminalItem) => {
  const theme = useTheme();

  return (
    <MotionBox
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.xxxl}
      maxHeight={"100%"}
      overflow={"hidden"}
      {...theme.tyle.animation.fade}
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
