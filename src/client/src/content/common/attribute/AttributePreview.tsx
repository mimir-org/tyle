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
} from "./AttributePreview.styled";

interface AttributePreviewProps {
  name: string;
  color: string;
  contents: InfoItem[];
  variant?: AttributePreviewVariant;
}

/**
 * Components which presents a visual representation of an attribute,
 * this includes a grid of key features and a variable amount of available values, references etc.
 *
 * @param name
 * @param color
 * @param contents
 * @param variant
 * @constructor
 */
export const AttributePreview = ({ name, color, contents, variant = "small" }: AttributePreviewProps) => {
  const theme = useTheme();
  const showContentButtons = contents.length > 0;
  const headerTextVariant: TextTypes = variant == "small" ? "title-small" : "title-medium";

  return (
    <AttributePreviewContainer variant={variant}>
      <AttributePreviewHeader bgColor={color}>
        <Text color={theme.tyle.color.ref.neutral["0"]} variant={headerTextVariant} useEllipsis>
          {name}
        </Text>
      </AttributePreviewHeader>

      <AttributeSpecificationContainer>
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
