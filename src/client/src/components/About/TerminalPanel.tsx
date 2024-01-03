import { Heading, MotionBox } from "@mimirorg/component-library";
import Flexbox from "components/Flexbox";
import InfoItemButton from "components/InfoItemButton";
import StateBadge from "components/StateBadge";
import TerminalPreview from "components/TerminalPreview";
import Text from "components/Text";
import { useTheme } from "styled-components";
import { TerminalItem } from "types/terminalItem";
import PanelPropertiesContainer from "./PanelPropertiesContainer";
import PanelSection from "./PanelSection";

/**
 * Component that displays information about a given terminal.
 *
 * @param props receives all properties of a TerminalItem
 * @constructor
 */
export const TerminalPanel = ({ name, description, color, attributes, tokens }: TerminalItem) => {
  const theme = useTheme();
  const showAttributes = attributes && attributes.length > 0;

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
      <TerminalPreview name={name} color={color} variant={"large"} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xl}>
        <Heading as={"h2"} variant={"title-large"} fontWeight={"500"} useEllipsis ellipsisMaxLines={2}>
          {name}
        </Heading>
        <Text useEllipsis ellipsisMaxLines={8}>
          {description}
        </Text>
      </Flexbox>
      <Flexbox gap={theme.tyle.spacing.xl} flexWrap={"wrap"}>
        {tokens && tokens.map((token, i) => <StateBadge state={token} key={token + i} />)}
      </Flexbox>

      <PanelPropertiesContainer>
        {showAttributes && (
          <PanelSection title="Attributes">
            {attributes.map((a, i) => (
              <InfoItemButton key={i} {...a} />
            ))}
          </PanelSection>
        )}
      </PanelPropertiesContainer>
    </MotionBox>
  );
};
