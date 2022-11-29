import { TransportItem } from "common/types/transportItem";
import { Token } from "complib/general";
import { Flexbox, MotionBox } from "complib/layouts";
import { Heading, Text } from "complib/text";
import { InfoItemButton } from "features/common/info-item";
import { TerminalPreview } from "features/common/terminal/TerminalPreview";
import { TransportPreview } from "features/common/transport";
import { PanelPropertiesContainer } from "features/explore/about/components/common/PanelPropertiesContainer";
import { PanelSection } from "features/explore/about/components/common/PanelSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

/**
 * Component that displays information about a given transport.
 *
 * @param props receives all properties of a TransportItem
 * @constructor
 */
export const TransportPanel = ({
  name,
  description,
  aspectColor,
  transportColor,
  attributes,
  terminal,
  tokens,
}: TransportItem) => {
  const theme = useTheme();
  const { t } = useTranslation();
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
      <TransportPreview variant={"large"} name={name} aspectColor={aspectColor} transportColor={transportColor} />

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
          <PanelSection title={t("attributes.title")}>
            {attributes.map((a, i) => (
              <InfoItemButton key={i} {...a} />
            ))}
          </PanelSection>
        )}
        <PanelSection title={t("transport.terminal")}>
          <TerminalPreview name={terminal.name} color={terminal.color} />
        </PanelSection>
      </PanelPropertiesContainer>
    </MotionBox>
  );
};
