import { TerminalItem } from "common/types/terminalItem";
import { Flexbox, Heading, MotionBox, Text } from "@mimirorg/component-library";
import { InfoItemButton } from "features/common/info-item";
import { TerminalPreview } from "features/entities/entityPreviews/terminal/TerminalPreview";
import { PanelPropertiesContainer } from "features/explore/about/components/common/PanelPropertiesContainer";
import { PanelSection } from "features/explore/about/components/common/PanelSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { AttributeGroupItem } from "common/types/attributeGroupItem";
import AttributeGroupPreview from "features/entities/entityPreviews/attributeGroup/AttributeGroupPreview";

/**
 * Component that displays information about a given terminal.
 *
 * @param props receives all properties of a TerminalItem
 * @constructor
 */
export const AttributeGroupPanel = ({ name, description, attributes }: AttributeGroupItem) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");

  const showAttributes = attributes && attributes.length > 0;

  console.log(attributes);

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
      <AttributeGroupPreview name={name} description={description} />

      <PanelPropertiesContainer>
        {showAttributes && (
          <PanelSection title={t("about.attributes")}>
            {attributes.map((a, i) => (
              <InfoItemButton descriptors={{}} key={i} {...a} />
            ))}
          </PanelSection>
        )}
      </PanelPropertiesContainer>
    </MotionBox>
  );
};
