import { AspectObjectTerminalItem } from "common/types/aspectObjectTerminalItem";
import { Td } from "complib/data-display";
import { Box } from "complib/layouts";
import { InfoItemButton } from "features/common/info-item";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

export const TerminalTableAttributes = ({ attributes }: Pick<AspectObjectTerminalItem, "attributes">) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");

  return (
    <Td data-label={t("about.terminals.templates.terminal", { object: t("about.terminals.attributes").toLowerCase() })}>
      <Box display={"flex"} flexWrap={"wrap"} minWidth={"200px"} gap={theme.tyle.spacing.base}>
        {attributes?.map((a) => (
          <InfoItemButton key={a.id} {...a} />
        ))}
      </Box>
    </Td>
  );
};
