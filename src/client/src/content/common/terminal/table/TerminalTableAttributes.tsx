import { useTheme } from "styled-components";
import textResources from "../../../../assets/text/TextResources";
import { Td } from "../../../../complib/data-display";
import { Box } from "../../../../complib/layouts";
import { TerminalItem } from "../../../types/TerminalItem";
import { AttributeInfoButton } from "../../attribute";

export const TerminalTableAttributes = ({ attributes }: Pick<TerminalItem, "attributes">) => {
  const theme = useTheme();

  return (
    <Td data-label={textResources.TERMINAL_TABLE_ATTRIBUTES}>
      <Box display={"flex"} flexWrap={"wrap"} minWidth={"200px"} gap={theme.tyle.spacing.base}>
        {attributes?.map((a, index) => (
          <AttributeInfoButton key={index} {...a} />
        ))}
      </Box>
    </Td>
  );
};