import { Box, Td } from "@mimirorg/component-library";
import InfoItemButton from "components/InfoItemButton";
import { useTheme } from "styled-components";
import { BlockTerminalItem } from "types/blockTerminalItem";

const TerminalTableAttributes = ({ attributes }: Pick<BlockTerminalItem, "attributes">) => {
  const theme = useTheme();

  return (
    <Td data-label="Terminal attributes">
      <Box display={"flex"} flexWrap={"wrap"} minWidth={"200px"} gap={theme.tyle.spacing.base}>
        {attributes?.map((a) => <InfoItemButton key={a.id} {...a} />)}
      </Box>
    </Td>
  );
};

export default TerminalTableAttributes;
