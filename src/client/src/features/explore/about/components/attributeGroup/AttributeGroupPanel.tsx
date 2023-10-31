import { MotionBox } from "@mimirorg/component-library";
import { InfoItemButton } from "components/InfoItemButton/InfoItemButton";
import { PanelPropertiesContainer } from "features/explore/about/components/common/PanelPropertiesContainer";
import { PanelSection } from "features/explore/about/components/common/PanelSection";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { AttributeGroupItem } from "common/types/attributeGroupItem";
import AttributeGroupPreview from "components/AttributeGroupPreview/AttributeGroupPreview";

/**
 * Component that displays information about a given AttributeGroup.
 *
 * @param props receives all properties of a AttributeGroupItem
 * @constructor
 */
export const AttributeGroupPanel = ({ name, description, attributes }: AttributeGroupItem) => {
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
