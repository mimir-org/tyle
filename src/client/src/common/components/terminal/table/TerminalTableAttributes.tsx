import { InfoItemButton } from "common/components/info-item";
import { NodeTerminalItem } from "common/types/nodeTerminalItem";
import { Td } from "complib/data-display";
import { Box } from "complib/layouts";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const TerminalTableAttributes = ({ attributes }: Pick<NodeTerminalItem, "attributes">) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });

  return (
    <Td data-label={t("templates.terminal", { object: t("attributes").toLowerCase() })}>
      <Box display={"flex"} flexWrap={"wrap"} minWidth={"200px"} gap={theme.tyle.spacing.base}>
        {attributes?.map((a, index) => (
          <InfoItemButton key={index} {...a} />
        ))}
      </Box>
    </Td>
  );
};
