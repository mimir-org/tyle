import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Td } from "../../../../complib/data-display";
import { Box } from "../../../../complib/layouts";
import { TerminalItem } from "../../../types/TerminalItem";
import { SelectItemInfoButton } from "../../selectItem";

export const TerminalTableAttributes = ({ attributes }: Pick<TerminalItem, "attributes">) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "terminals" });

  return (
    <Td data-label={t("templates.terminal", { object: t("attributes").toLowerCase() })}>
      <Box display={"flex"} flexWrap={"wrap"} minWidth={"200px"} gap={theme.tyle.spacing.base}>
        {attributes?.map((a, index) => (
          <SelectItemInfoButton key={index} {...a} />
        ))}
      </Box>
    </Td>
  );
};
