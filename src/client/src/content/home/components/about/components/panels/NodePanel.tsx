import { useTheme } from "styled-components";
import textResources from "../../../../../../assets/text/TextResources";
import { Token } from "../../../../../../complib/general";
import { Box, Flexbox, MotionBox } from "../../../../../../complib/layouts";
import { Heading, Text } from "../../../../../../complib/text";
import { AttributeItem } from "../../../../types/AttributeItem";
import { NodeItem } from "../../../../types/NodeItem";
import { TerminalItem } from "../../../../types/TerminalItem";
import { AttributeInfoButton } from "../attribute/AttributeInfoButton";
import { NodePreview } from "../node/NodePreview";
import { TerminalTable } from "../terminal/TerminalTable";
import { NodePanelPropertiesContainer } from "./NodePanel.styled";

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
      gap={theme.tyle.spacing.xxl}
      maxHeight={"100%"}
      overflow={"hidden"}
      {...theme.tyle.animation.fade}
    >
      <NodePreview variant={"large"} name={name} color={color} img={img} terminals={terminals} />

      <Heading as={"h2"} variant={"headline-medium"} useEllipsis ellipsisMaxLines={2}>
        {name}
      </Heading>
      <Text useEllipsis ellipsisMaxLines={5}>
        {description}
      </Text>
      <Flexbox gap={theme.tyle.spacing.xl} flexWrap={"wrap"}>
        {tokens && tokens.map((t, i) => <Token key={i}>{t}</Token>)}
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
      <Box display={"flex"} gap={theme.tyle.spacing.xl} flexWrap={"wrap"}>
        {attributes && attributes.map((a, i) => <AttributeInfoButton key={i} {...a} />)}
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
