import { AnimatePresence } from "framer-motion";
import { useTheme } from "styled-components";
import { TextResources } from "../../../../assets/text";
import { Box, MotionBox } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { useGetNode } from "../../../../data/queries/tyle/queriesNode";
import { mapNodeLibCmToNodeItem } from "../../../../utils/mappers";
import { NodePanel } from "./components/panels/NodePanel";

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
  const showNodePanel = !nodeQuery.isFetching && !nodeQuery.isLoading && nodeQuery.isSuccess;
  const showPlaceHolder = nodeQuery.isIdle || nodeQuery.isError;

  return (
    <Box
      as={"section"}
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.xxxl}
      pt={theme.tyle.spacing.multiple(6)}
      px={theme.tyle.spacing.xxxl}
      pb={theme.tyle.spacing.xl}
      height={"100%"}
      minWidth={"400px"}
      color={theme.tyle.color.sys.background.on}
    >
      <Text variant={"headline-large"}>{TextResources.ABOUT_TITLE}</Text>
      <AnimatePresence exitBeforeEnter>
        {showPlaceHolder && <Placeholder text={TextResources.ABOUT_PLACEHOLDER} />}
        {showNodePanel && <NodePanel key={nodeQuery.data.id} {...mapNodeLibCmToNodeItem(nodeQuery.data)} />}
      </AnimatePresence>
    </Box>
  );
};

const Placeholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionBox
      display={"flex"}
      flex={1}
      justifyContent={"center"}
      alignItems={"center"}
      {...theme.tyle.animation.fade}
      color={theme.tyle.color.sys.outline.base}
    >
      <Text variant={"title-large"}>{text}</Text>
    </MotionBox>
  );
};
