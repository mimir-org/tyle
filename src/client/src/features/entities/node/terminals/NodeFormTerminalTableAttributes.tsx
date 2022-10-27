import { TerminalLibCm } from "@mimirorg/typelibrary-types";
import { InfoItemButton } from "common/components/info-item";
import { useMediaQuery } from "common/hooks/useMediaQuery";
import { mapAttributeLibCmToInfoItem } from "common/utils/mappers";
import { Td } from "complib/data-display";
import { Box } from "complib/layouts";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const NodeFormTerminalTableAttributes = ({ attributes }: Pick<TerminalLibCm, "attributes">) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });
  const adjustAttributesPadding = useMediaQuery("screen and (min-width: 1500px)");

  return (
    <Td data-label={t("templates.terminal", { object: t("attributes").toLowerCase() })}>
      <Box
        display={"flex"}
        flexWrap={"wrap"}
        gap={theme.tyle.spacing.base}
        pl={adjustAttributesPadding ? theme.tyle.spacing.xxxl : undefined}
      >
        {attributes.map((x) => x && <InfoItemButton key={x.id} {...mapAttributeLibCmToInfoItem(x)} />)}
      </Box>
    </Td>
  );
};
