import {
  mapNodeLibCmToNodeItem,
  mapTerminalLibCmToTerminalItem,
} from "common/utils/mappers";
import { useGetNode } from "external/sources/node/node.queries";
import { useGetTerminal } from "external/sources/terminal/terminal.queries";
import { Loader } from "features/common/loader";
import { AboutPlaceholder } from "features/explore/about/components/AboutPlaceholder";
import { NodePanel } from "features/explore/about/components/node/NodePanel";
import { TerminalPanel } from "features/explore/about/components/terminal/TerminalPanel";
import { ExploreSection } from "features/explore/common/ExploreSection";
import { SelectedInfo } from "features/explore/common/selectedInfo";
import { AnimatePresence } from "framer-motion";
import { useTranslation } from "react-i18next";

interface AboutProps {
  selected?: SelectedInfo;
}

/**
 * Component which houses info-panels that display various data associated with the selected item.
 *
 * @param selected the currently selected item
 * @constructor
 */
export const About = ({ selected }: AboutProps) => {
  const { t } = useTranslation("explore");

  const nodeQuery = useGetNode(selected?.type == "node" ? selected?.id : "");
  const terminalQuery = useGetTerminal(selected?.type == "terminal" ? selected?.id : "");
  const allQueries = [nodeQuery, terminalQuery];

  const showLoader = allQueries.some((x) => x.isFetching);
  const showPlaceHolder = !showLoader && allQueries.every((x) => !x.isFetched);

  const showNodePanel = !showLoader && nodeQuery.isSuccess;
  const showTerminalPanel = !showLoader && terminalQuery.isSuccess;

  return (
    <ExploreSection title={t("about.title")}>
      <AnimatePresence mode={"wait"}>
        {showLoader && <Loader />}
        {showPlaceHolder && <AboutPlaceholder text={t("about.placeholders.item")} />}
        {showNodePanel && <NodePanel key={nodeQuery.data.id} {...mapNodeLibCmToNodeItem(nodeQuery.data)} />}
        {showTerminalPanel && (
          <TerminalPanel key={terminalQuery.data.id} {...mapTerminalLibCmToTerminalItem(terminalQuery.data)} />
        )}
      </AnimatePresence>
    </ExploreSection>
  );
};
