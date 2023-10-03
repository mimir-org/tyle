import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { mapAttributeLibCmToInfoItem } from "common/utils/mappers";
import { Box, FormField } from "@mimirorg/component-library";
import { InfoItemButton } from "features/common/info-item";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

interface BlockTerminalAttributesProps {
  attributes: AttributeLibCm[];
  hideLabel?: boolean;
}
export const BlockTerminalAttributes = ({ attributes, hideLabel }: BlockTerminalAttributesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const showAttributes = attributes && attributes.length > 0;

  return (
    <>
      {showAttributes && (
        <FormField indent={false} label={!hideLabel ? t("block.terminals.attributes") : undefined}>
          <Box
            display={"flex"}
            flexWrap={"wrap"}
            alignItems={"center"}
            gap={theme.mimirorg.spacing.base}
            minHeight={"40px"}
          >
            {attributes.map((x) => x && <InfoItemButton key={x.id} {...mapAttributeLibCmToInfoItem(x)} />)}
          </Box>
        </FormField>
      )}
    </>
  );
};
