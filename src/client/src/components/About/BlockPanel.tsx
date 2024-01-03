import { MotionBox } from "components/Box";
import { useTheme } from "styled-components";
import { BlockItem } from "types/blockItem";
import PreviewPanel from "./PreviewPanel";

/**
 * Component that displays information about a given block.
 *
 * @param name
 * @param description
 * @param img
 * @param tokens
 * @param terminals
 * @param attributes
 * @constructor
 */
const BlockPanel = ({ name, description, tokens, terminals, attributes, kind }: BlockItem) => {
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
        terminals={terminals}
        attributes={attributes}
        kind={kind}
        state={""}
      />
    </MotionBox>
  );
};

export default BlockPanel;
