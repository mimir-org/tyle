import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Box } from "../../../complib/layouts";
import { Text } from "../../../complib/text";
import { InfoItem } from "../../types/InfoItem";
import { InfoItemButton } from "../infoItem";
import { AttributePreviewContainer, AttributePreviewDivider, AttributePreviewHeader } from "./AttributePreview.styled";

interface AttributePreviewProps {
  name: string;
  color: string;
  qualifier: string;
  source: string;
  condition: string;
  contents: InfoItem[];
}

/**
 * Components that presents a visual representation of an attribute,
 * which includes a grid of key features and a variable amount of available values, references etc.
 *
 * @param name
 * @param color
 * @param source
 * @param qualifier
 * @param condition
 * @param contents
 * @constructor
 */
export const AttributePreview = ({ name, color, source, qualifier, condition, contents }: AttributePreviewProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "attribute" });

  return (
    <AttributePreviewContainer>
      <AttributePreviewHeader bgColor={color}>
        <Text color={theme.tyle.color.ref.neutral["0"]} variant={"title-medium"} useEllipsis>
          {name}
        </Text>
      </AttributePreviewHeader>

      <Box
        flex={1}
        display={"flex"}
        flexDirection={"column"}
        justifyContent={"space-between"}
        gap={theme.tyle.spacing.xl}
        p={theme.tyle.spacing.l}
        pl={theme.tyle.spacing.xl}
        pr={theme.tyle.spacing.xl}
      >
        <Box display={"grid"} gridTemplateColumns={"1fr 1fr 1fr"} gap={theme.tyle.spacing.s}>
          <Text variant={"label-medium"} textTransform={"capitalize"}>
            {t("qualifier")}
          </Text>
          <Text variant={"label-medium"} textTransform={"capitalize"}>
            {t("source")}
          </Text>
          <Text variant={"label-medium"} textTransform={"capitalize"}>
            {t("condition")}
          </Text>
          <AttributePreviewDivider />
          <Text variant={"label-small"}>{qualifier}</Text>
          <Text variant={"label-small"}>{source}</Text>
          <Text variant={"label-small"}>{condition}</Text>
        </Box>

        <Box display={"flex"} flexWrap={"wrap"} justifyContent={"center"} gap={theme.tyle.spacing.s}>
          {contents.map((info, i) => {
            const showDescriptor = Object.keys(info.descriptors).length > 0;
            return showDescriptor && <InfoItemButton key={i} name={info.name} descriptors={info.descriptors} />;
          })}
        </Box>
      </Box>
    </AttributePreviewContainer>
  );
};
