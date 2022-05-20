import textResources from "../../../../../../assets/text/TextResources";
import { useTheme } from "styled-components";
import { NodePanelPropertiesContainer } from "./NodePanel.styled";
import { Box, Flexbox, MotionBox } from "../../../../../../complib/layouts";
import { Heading, Text } from "../../../../../../complib/text";
import { Token } from "../../../../../../complib/general/token/Token";
import { NodePreview } from "../node/NodePreview";
import { NodeItem } from "../../../../types/NodeItem";
import { AttributeSingle } from "../attribute/AttributeSingle";
import { TerminalTable } from "../terminal/TerminalTable";
import { AttributeItem } from "../../../../types/AttributeItem";
import { TerminalItem } from "../../../../types/TerminalItem";

/**
 * Component that displays information about a given node.
 *
 * @param name
 * @param description
 * @param img
 * @param color
 * @param tokens
 * @param terminals
 * @param attributes
 * @constructor
 */
export const NodePanel = ({ name, description, img, color, tokens, terminals, attributes }: NodeItem) => {
  const theme = useTheme();
  const showTerminals = terminals && terminals.length > 0;
  const showAttributes = attributes && attributes.length > 0;

  return (
    <MotionBox
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.large}
      maxHeight={"100%"}
      overflow={"hidden"}
      {...theme.tyle.animation.fade}
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

      <NodePanelPropertiesContainer>
        {showAttributes && <NodePanelAttributes attributes={attributes} />}
        {showTerminals && <NodePanelTerminals terminals={terminals} />}
      </NodePanelPropertiesContainer>
    </MotionBox>
  );
};

const NodePanelAttributes = ({ attributes }: { attributes: AttributeItem[] }) => {
  const theme = useTheme();

  return (
    <>
      <Heading as={"h3"} variant={"headline-small"}>
        {textResources.ATTRIBUTE_TITLE}
      </Heading>
      <Box display={"flex"} gap={theme.tyle.spacing.medium} flexWrap={"wrap"}>
        {attributes && attributes.map((a, i) => <AttributeSingle key={i} {...a} />)}
      </Box>
    </>
  );
};

const NodePanelTerminals = ({ terminals }: { terminals: TerminalItem[] }) => (
  <>
    <Heading as={"h3"} variant={"headline-small"}>
      {textResources.TERMINAL_TITLE}
    </Heading>
    <TerminalTable terminals={terminals} />
  </>
);
