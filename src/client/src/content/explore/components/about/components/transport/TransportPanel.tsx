import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Token } from "../../../../../../complib/general";
import { Flexbox, MotionBox } from "../../../../../../complib/layouts";
import { Heading, Text } from "../../../../../../complib/text";
import { InfoItemButton } from "../../../../../common/infoItem";
import { TransportPreview } from "../../../../../common/transport";
import { TransportItem } from "../../../../../types/TransportItem";
import { PanelPropertiesContainer } from "../common/PanelPropertiesContainer";
import { PanelSection } from "../common/PanelSection";

/**
 * Component that displays information about a given transport.
 *
 * @param props receives all properties of a TransportItem
 * @constructor
 */
export const TransportPanel = ({ name, description, color, attributes, tokens }: TransportItem) => {
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
      <TransportPreview name={name} color={color} variant={"large"} />

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
      </PanelPropertiesContainer>
    </MotionBox>
  );
};
