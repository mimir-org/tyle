import { Flexbox, Heading, MotionBox, Text } from "@mimirorg/component-library";
import { TerminalItem } from "common/types/terminalItem";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import InfoItemButton from "../InfoItemButton";
import StateBadge from "../StateBadge";
import TerminalPreview from "../TerminalPreview";
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
  const { t } = useTranslation("explore");
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
      <TerminalPreview name={name} color={color} variant={"large"} />

      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.xl}>
        <Heading as={"h2"} variant={"title-large"} fontWeight={"500"} useEllipsis ellipsisMaxLines={2}>
          {name}
        </Heading>
        <Text useEllipsis ellipsisMaxLines={8}>
          {description}
        </Text>
      </Flexbox>
      <Flexbox gap={theme.mimirorg.spacing.xl} flexWrap={"wrap"}>
        {tokens && tokens.map((token, i) => <StateBadge state={token} key={token + i} />)}
      </Flexbox>

      <PanelPropertiesContainer>
        {showAttributes && (
          <PanelSection title={t("about.attributes")}>
            {attributes.map((a, i) => (
              <InfoItemButton key={i} {...a} />
            ))}
          </PanelSection>
        )}
      </PanelPropertiesContainer>
    </MotionBox>
  );
};
