import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { TextTypes } from "../../../complib/props";
import { Text } from "../../../complib/text";
import { InfoItem } from "../../types/InfoItem";
import { InfoItemButton } from "../info-item";
import {
  AttributePreviewContainer,
  AttributePreviewHeader,
  AttributePreviewVariant,
  AttributeSpecificationContainer,
  AttributeSpecificationGrid,
  AttributeSpecificationGridDivider,
} from "./AttributePreview.styled";

interface AttributePreviewProps {
  name: string;
  color: string;
  qualifier: string;
  source: string;
  condition: string;
  contents: InfoItem[];
  variant?: AttributePreviewVariant;
}

/**
 * Components which presents a visual representation of an attribute,
 * this includes a grid of key features and a variable amount of available values, references etc.
 *
 * @param name
 * @param color
 * @param source
 * @param qualifier
 * @param condition
 * @param contents
 * @param variant
 * @constructor
 */
export const AttributePreview = ({
  name,
  color,
  source,
  qualifier,
  condition,
  contents,
  variant = "small",
}: AttributePreviewProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "attribute" });
  const showContentButtons = contents.length > 0;
  const headerTextVariant: TextTypes = variant == "small" ? "title-small" : "title-medium";
  const gridTitleTextVariant: TextTypes = variant == "small" ? "label-small" : "label-medium";

  return (
    <AttributePreviewContainer variant={variant}>
      <AttributePreviewHeader bgColor={color}>
        <Text color={theme.tyle.color.ref.neutral["0"]} variant={headerTextVariant} useEllipsis>
          {name}
        </Text>
      </AttributePreviewHeader>

      <AttributeSpecificationContainer>
        <AttributeSpecificationGrid layout={"position"}>
          <Text variant={gridTitleTextVariant}>{t("qualifier")}</Text>
          <Text variant={gridTitleTextVariant}>{t("source")}</Text>
          <Text variant={gridTitleTextVariant}>{t("condition")}</Text>
          <AttributeSpecificationGridDivider />
          <Text variant={"label-small"}>{qualifier}</Text>
          <Text variant={"label-small"}>{source}</Text>
          <Text variant={"label-small"}>{condition}</Text>
        </AttributeSpecificationGrid>

        {showContentButtons && (
          <Box display={"flex"} flexWrap={"wrap"} justifyContent={"center"} gap={theme.tyle.spacing.s}>
            {contents.map((info, i) => {
              const showDescriptor = Object.keys(info.descriptors).length > 0;
              return showDescriptor && <InfoItemButton key={i} name={info.name} descriptors={info.descriptors} />;
            })}
          </Box>
        )}
      </AttributeSpecificationContainer>
    </AttributePreviewContainer>
  );
};
