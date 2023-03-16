import { mapNodeLibCmToNodeItem, mapTerminalLibCmToTerminalItem } from "common/utils/mappers";
import { useGetNode } from "external/sources/node/node.queries";
import { useGetTerminal } from "external/sources/terminal/terminal.queries";
import { Loader } from "features/common/loader";
import { AboutPlaceholder } from "features/explore/about/components/AboutPlaceholder";
import { NodePanel } from "features/explore/about/components/node/NodePanel";
import { TerminalPanel } from "features/explore/about/components/terminal/TerminalPanel";
import { ExploreSection } from "features/explore/common/ExploreSection";
import { SelectedInfo } from "features/explore/common/selectedInfo";
import { useEffect, useState } from "react";
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

  const nodeQuery = useGetNode(selected?.type === "node" ? selected?.id : undefined);
  const terminalQuery = useGetTerminal(selected?.type == "terminal" ? selected?.id : undefined);

  const [showLoader, setShowLoader] = useState(true);

  useEffect(() => {
    const allQueries = [nodeQuery, terminalQuery];
    setShowLoader(allQueries.some((x) => x.isFetching));
  }, [nodeQuery, terminalQuery]);

  const showPlaceHolder = !showLoader && selected?.type === undefined;
  const showNodePanel = !showLoader && selected?.type === "node" && nodeQuery.isSuccess;
  const showTerminalPanel = !showLoader && selected?.type === "terminal" && terminalQuery.isSuccess;

  return (
    <ExploreSection title={t("about.title")}>
      {showLoader && <Loader />}
      {showPlaceHolder && <AboutPlaceholder text={t("about.placeholders.item")} />}
      {showNodePanel && <NodePanel key={nodeQuery.data.id} {...mapNodeLibCmToNodeItem(nodeQuery.data)} />}
      {showTerminalPanel && (
        <TerminalPanel key={terminalQuery.data.id} {...mapTerminalLibCmToTerminalItem(terminalQuery.data)} />
      )}
    </ExploreSection>
  );
};
