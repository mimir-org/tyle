import { useTheme } from "styled-components";
import { useGetNode } from "../../../../data/queries/tyle/queriesNode";
import { Box } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { AnimatePresence } from "framer-motion";
import { NodePanel } from "./components/panels/NodePanel";
import { TextResources } from "../../../../assets/text";
import { mapNodeLibCmToNodeItem } from "../../../../utils/mappers";

interface AboutProps {
  selected?: string;
}

/**
 * Component which houses info-panels that display various data associated with the selected item.
 *
 * @param selected the currently selected item
 * @constructor
 */
export const About = ({ selected }: AboutProps) => {
  const theme = useTheme();
  const nodeQuery = useGetNode(selected);

  return (
    <Box
      as={"section"}
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.large}
      pt={theme.tyle.spacing.xl}
      px={theme.tyle.spacing.large}
      pb={theme.tyle.spacing.medium}
      height={"100%"}
      minWidth={"400px"}
      bgColor={theme.tyle.color.surface.variant.base}
      color={theme.tyle.color.surface.variant.on}
    >
      <Text variant={"display-small"}>{TextResources.ABOUT_TITLE}</Text>
      {nodeQuery.isIdle && <Placeholder text={TextResources.ABOUT_PLACEHOLDER} />}
      <AnimatePresence exitBeforeEnter>
        {!nodeQuery.isFetching && !nodeQuery.isLoading && nodeQuery.isSuccess && (
          <NodePanel key={`NodePreview-${nodeQuery.data.id}`} node={mapNodeLibCmToNodeItem(nodeQuery.data)} />
        )}
      </AnimatePresence>
    </Box>
  );
};

const Placeholder = ({ text }: { text: string }) => (
  <Box display={"flex"} flex={1} justifyContent={"center"} alignItems={"center"}>
    <Text variant={"headline-large"}>{text}</Text>
  </Box>
);
