import { AnimatePresence } from "framer-motion";
import { useTheme } from "styled-components";
import { TextResources } from "../../../../assets/text";
import { MotionBox } from "../../../../complib/layouts";
import { Text } from "../../../../complib/text";
import { useGetNode } from "../../../../data/queries/tyle/queriesNode";
import { mapNodeLibCmToNodeItem } from "../../../../utils/mappers";
import { HomeSection } from "../HomeSection";
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
  const nodeQuery = useGetNode(selected);
  const showNodePanel = !nodeQuery.isFetching && !nodeQuery.isLoading && nodeQuery.isSuccess && nodeQuery.data;
  const showPlaceHolder = nodeQuery.isIdle || nodeQuery.isError || !nodeQuery.data;

  return (
    <HomeSection title={TextResources.ABOUT_TITLE}>
      <AnimatePresence exitBeforeEnter>
        {showPlaceHolder && <Placeholder text={TextResources.ABOUT_PLACEHOLDER} />}
        {showNodePanel && <NodePanel key={nodeQuery.data.id} {...mapNodeLibCmToNodeItem(nodeQuery.data)} />}
      </AnimatePresence>
    </HomeSection>
  );
};

const Placeholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionBox display={"flex"} flex={1} justifyContent={"center"} alignItems={"center"} {...theme.tyle.animation.fade}>
      <Text variant={"title-large"} color={theme.tyle.color.sys.surface.on}>
        {text}
      </Text>
    </MotionBox>
  );
};
