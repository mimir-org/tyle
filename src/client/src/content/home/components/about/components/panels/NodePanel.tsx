import { useTheme } from "styled-components";
import { Flexbox, MotionBox } from "../../../../../../complib/layouts";
import { Heading, Text } from "../../../../../../complib/text";
import { Token } from "../../../../../../complib/general/token/Token";
import { NodePreview } from "../node/NodePreview";
import { NodeItem } from "../../../../types/NodeItem";

interface NodePanelProps {
  node: NodeItem;
}

/**
 * Component that displays information about a given node.
 *
 * @param node
 * @constructor
 */
export const NodePanel = ({ node }: NodePanelProps) => {
  const theme = useTheme();
  const { name, description, img, color, tokens, terminals } = node;

  return (
    <MotionBox
      {...theme.tyle.animation.fade}
      display={"flex"}
      flex={1}
      flexDirection={"column"}
      gap={theme.tyle.spacing.large}
      maxHeight={"100%"}
    >
      <NodePreview color={color} img={img} terminals={terminals} />

      <Flexbox gap={theme.tyle.spacing.large} flexDirection={"column"}>
        <Heading as={"h2"} variant={"headline-medium"} useEllipsis ellipsisMaxLines={2}>
          {name}
        </Heading>
        <Text useEllipsis ellipsisMaxLines={5}>
          {description}
        </Text>
        <Flexbox gap={theme.tyle.spacing.medium} flexWrap={"wrap"}>
          {tokens && tokens.map((t, i) => <Token key={i} text={t} />)}
        </Flexbox>
      </Flexbox>
    </MotionBox>
  );
};
