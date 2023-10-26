import { BlockItem } from "common/types/blockItem";
import { Heading, Flexbox, MotionBox, Text } from "@mimirorg/component-library";
import { InfoItemButton } from "features/common/info-item";
import { PanelPropertiesContainer } from "features/explore/about/components/common/PanelPropertiesContainer";
import { PanelSection } from "features/explore/about/components/common/PanelSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { TerminalTable } from "./terminal-table/TerminalTable";
// import { BlockPreview } from "../../../../entities/entityPreviews/block/BlockPreview";
import { StateBadge } from "../../../../ui/badges/StateBadge";

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
export const BlockPanel = ({
  name,
  description,
  // img, color,
  tokens,
  terminals,
  attributes,
}: BlockItem) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");
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
          <PanelSection title={t("about.attributes")}>
            {attributes.map((a) => (
              <InfoItemButton key={a.id} {...a} />
            ))}
          </PanelSection>
        )}
        {showTerminals && (
          <PanelSection title={t("about.terminals.title")}>
            <TerminalTable terminals={terminals} />
          </PanelSection>
        )}
      </PanelPropertiesContainer>
    </MotionBox>
  );
};
