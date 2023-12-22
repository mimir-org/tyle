import { Flexbox, Heading, MotionBox, Text } from "@mimirorg/component-library";
import InfoItemButton from "components/InfoItemButton";
import StateBadge from "components/StateBadge";
import { useTheme } from "styled-components";
import { BlockItem } from "types/blockItem";
import PanelPropertiesContainer from "./PanelPropertiesContainer";
import PanelSection from "./PanelSection";
import TerminalTable from "./TerminalTable";

/**
 * Component that displays information about a given block.
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
const BlockPanel = ({
  name,
  description,
  // img, color,
  tokens,
  terminals,
  attributes,
}: BlockItem) => {
  const theme = useTheme();
  const showTerminals = terminals && terminals.length > 0;
  const showAttributes = attributes && attributes.length > 0;

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
      {/* <BlockPreview variant={"large"} name={name} color={color} img={img} terminals={terminals} /> */}

      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xl}>
        <Heading as={"h2"} variant={"title-large"} fontWeight={"500"} useEllipsis ellipsisMaxLines={2}>
          {name}
        </Heading>
        <Text useEllipsis ellipsisMaxLines={8}>
          {description}
        </Text>
      </Flexbox>
      <Flexbox gap={theme.mimirorg.spacing.xl} flexWrap={"wrap"}>
        {tokens && tokens.map((token, i) => <StateBadge key={i + token} state={token} />)}
      </Flexbox>

      <PanelPropertiesContainer>
        {showAttributes && (
          <PanelSection title="Attributes">
            {attributes.map((a) => (
              <InfoItemButton key={a.id} {...a} />
            ))}
          </PanelSection>
        )}
        {showTerminals && (
          <PanelSection title="Terminals">
            <TerminalTable terminals={terminals} />
          </PanelSection>
        )}
      </PanelPropertiesContainer>
    </MotionBox>
  );
};

export default BlockPanel;
