import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { mapAttributeLibCmToInfoItem } from "common/utils/mappers";
import { FormField } from "complib/form";
import { Box } from "complib/layouts";
import { InfoItemButton } from "features/common/info-item";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

interface AspectObjectTerminalAttributesProps {
  attributes: AttributeLibCm[];
  hideLabel?: boolean;
}
export const AspectObjectTerminalAttributes = ({ attributes, hideLabel }: AspectObjectTerminalAttributesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const showAttributes = attributes && attributes.length > 0;

  return (
    <>
      {showAttributes && (
        <FormField indent={false} label={!hideLabel ? t("aspectObject.terminals.attributes") : undefined}>
          <Box
            display={"flex"}
            flexWrap={"wrap"}
            alignItems={"center"}
            gap={theme.tyle.spacing.base}
            minHeight={"40px"}
          >
            {attributes.map((x, i) => x && <InfoItemButton key={`${i},${x.id}`} {...mapAttributeLibCmToInfoItem(x)} />)}
          </Box>
        </FormField>
      )}
    </>
  );
};
