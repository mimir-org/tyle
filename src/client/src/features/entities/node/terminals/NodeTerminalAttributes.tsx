import { TerminalLibCm } from "@mimirorg/typelibrary-types";
import { mapAttributeLibCmToInfoItem } from "common/utils/mappers";
import { FormField } from "complib/form";
import { Box } from "complib/layouts";
import { InfoItemButton } from "features/common/info-item";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const NodeTerminalAttributes = ({ attributes }: Pick<TerminalLibCm, "attributes">) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const showAttributes = attributes && attributes.length > 0;

  return (
    <>
      {showAttributes && (
        <FormField indent={false} label={t("node.terminals.attributes")}>
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
