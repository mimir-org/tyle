import { InfoItemButton } from "common/components/info-item";
import { InterfacePreview } from "common/components/interface";
import { TerminalPreview } from "common/components/terminal/TerminalPreview";
import { InterfaceItem } from "common/types/interfaceItem";
import { Token } from "complib/general";
import { Flexbox, MotionBox } from "complib/layouts";
import { Heading, Text } from "complib/text";
import { PanelPropertiesContainer } from "features/explore/about/components/common/PanelPropertiesContainer";
import { PanelSection } from "features/explore/about/components/common/PanelSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

/**
 * Component that displays information about a given interface.
 *
 * @param props receives all properties of a InterfaceItem
 * @constructor
 */
export const InterfacePanel = ({
  name,
  description,
  aspectColor,
  interfaceColor,
  attributes,
  terminal,
  tokens,
}: InterfaceItem) => {
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
      <InterfacePreview variant={"large"} name={name} aspectColor={aspectColor} interfaceColor={interfaceColor} />

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
        <PanelSection title={t("interface.terminal")}>
          <TerminalPreview name={terminal.name} color={terminal.color} />
        </PanelSection>
      </PanelPropertiesContainer>
    </MotionBox>
  );
};
