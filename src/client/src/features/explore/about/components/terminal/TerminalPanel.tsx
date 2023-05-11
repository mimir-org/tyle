import { TerminalItem } from "common/types/terminalItem";
import { Token } from "complib/general";
import { Flexbox, MotionBox } from "complib/layouts";
import { Heading, Text } from "complib/text";
import { InfoItemButton } from "features/common/info-item";
import { TerminalPreview } from "features/entities/entityPreviews/terminal/TerminalPreview";
import { PanelPropertiesContainer } from "features/explore/about/components/common/PanelPropertiesContainer";
import { PanelSection } from "features/explore/about/components/common/PanelSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

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
        {tokens && tokens.map((token, i) => <Token key={i}>{token}</Token>)}
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
