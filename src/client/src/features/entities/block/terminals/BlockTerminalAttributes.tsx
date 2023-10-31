import { Box, FormField } from "@mimirorg/component-library";
import { InfoItemButton } from "components/InfoItemButton/InfoItemButton";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { mapAttributeViewToInfoItem } from "common/utils/mappers/mapAttributeLibCmToInfoItem";
import { AttributeView } from "common/types/attributes/attributeView";

interface BlockTerminalAttributesProps {
  attributes: AttributeView[];
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
            {attributes.map((x) => x && <InfoItemButton key={x.id} {...mapAttributeViewToInfoItem(x)} />)}
          </Box>
        </FormField>
      )}
    </>
  );
};
