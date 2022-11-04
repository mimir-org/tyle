import { TerminalLibCm } from "@mimirorg/typelibrary-types";
import { InfoItemButton } from "common/components/info-item";
import { mapAttributeLibCmToInfoItem } from "common/utils/mappers";
import { FormField } from "complib/form";
import { Box } from "complib/layouts";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const NodeTerminalAttributes = ({ attributes }: Pick<TerminalLibCm, "attributes">) => {
  const theme = useTheme();
  const { t } = useTranslation();
  const showAttributes = attributes && attributes.length > 0;

  return (
    <>
      {showAttributes && (
        <FormField indent={false} label={t("terminals.attributes")}>
          <Box
            display={"flex"}
            flexWrap={"wrap"}
            alignItems={"center"}
            gap={theme.tyle.spacing.base}
            minHeight={"40px"}
          >
            {attributes.map((x) => x && <InfoItemButton key={x.id} {...mapAttributeLibCmToInfoItem(x)} />)}
          </Box>
        </FormField>
      )}
    </>
  );
};
