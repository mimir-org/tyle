import { useTheme } from "styled-components";
import { Box, Flexbox, MotionBox } from "../../../../../../complib/layouts";
import { Heading, Text } from "../../../../../../complib/text";
import { Token } from "../../../../../../complib/general/token/Token";
import { NodePreview } from "../node/NodePreview";
import { NodeItem } from "../../../../types/NodeItem";
import { AttributeSingle } from "../attribute/AttributeSingle";

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
  const { name, description, img, color, tokens, terminals, attributes } = node;

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

      <Heading as={"h2"} variant={"headline-medium"} useEllipsis ellipsisMaxLines={2}>
        {name}
      </Heading>
      <Text useEllipsis ellipsisMaxLines={5}>
        {description}
      </Text>
      <Flexbox gap={theme.tyle.spacing.medium} flexWrap={"wrap"}>
        {tokens && tokens.map((t, i) => <Token key={i} text={t} />)}
      </Flexbox>

      <Heading as={"h2"} variant={"headline-medium"}>
        Attributes
      </Heading>
      <Box display={"flex"} gap={theme.tyle.spacing.medium} flexWrap={"wrap"} maxHeight={"200px"} overflow={"auto"}>
        {attributes && attributes.map((a, i) => <AttributeSingle key={i} {...a} />)}
      </Box>

      <Box></Box>
    </MotionBox>
  );
};
