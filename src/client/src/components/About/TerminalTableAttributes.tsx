import { Box, Td } from "@mimirorg/component-library";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import InfoItemButton from "../InfoItemButton";

const TerminalTableAttributes = ({ attributes }: Pick<BlockTerminalItem, "attributes">) => {
  const theme = useTheme();
  const { t } = useTranslation("explore");

  return (
    <Td data-label={t("about.terminals.templates.terminal", { object: t("about.terminals.attributes").toLowerCase() })}>
      <Box display={"flex"} flexWrap={"wrap"} minWidth={"200px"} gap={theme.mimirorg.spacing.base}>
        {attributes?.map((a) => <InfoItemButton key={a.id} {...a} />)}
      </Box>
    </Td>
  );
};

export default TerminalTableAttributes;
