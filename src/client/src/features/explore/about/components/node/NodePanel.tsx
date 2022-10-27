import { Token } from "complib/general";
import { Flexbox, MotionBox } from "complib/layouts";
import { Heading, Text } from "complib/text";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { InfoItemButton } from "../../../../../common/components/info-item";
import { NodePreview } from "../../../../../common/components/node";
import { TerminalTable } from "../../../../../common/components/terminal";
import { NodeItem } from "../../../../../common/types/nodeItem";
import { PanelPropertiesContainer } from "../common/PanelPropertiesContainer";
import { PanelSection } from "../common/PanelSection";

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
  const { t } = useTranslation();
  const showTerminals = terminals && terminals.length > 0;
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
      <NodePreview variant={"large"} name={name} color={color} img={img} terminals={terminals} />

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
        {showTerminals && (
          <PanelSection title={t("terminals.title")}>
            <TerminalTable terminals={terminals} />
          </PanelSection>
        )}
      </PanelPropertiesContainer>
    </MotionBox>
  );
};
