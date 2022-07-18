import { useTheme } from "styled-components";
import textResources from "../../../../../../assets/text/TextResources";
import { Box } from "../../../../../../complib/layouts";
import { Heading } from "../../../../../../complib/text";
import { AttributeInfoButton } from "../../../../../common/attribute";
import { NodeItem } from "../../../../../types/NodeItem";

export const NodePanelAttributes = ({ attributes }: Pick<NodeItem, "attributes">) => {
  const theme = useTheme();

  return (
    <>
      <Heading as={"h3"} variant={"body-large"} color={theme.tyle.color.sys.surface.on}>
        {textResources.ATTRIBUTE_TITLE}
      </Heading>
      <Box display={"flex"} gap={theme.tyle.spacing.xl} flexWrap={"wrap"}>
        {attributes && attributes.map((a, i) => <AttributeInfoButton key={i} {...a} />)}
      </Box>
    </>
  );
};
