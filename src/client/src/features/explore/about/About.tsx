import {
  mapInterfaceLibCmToInterfaceItem,
  mapNodeLibCmToNodeItem,
  mapTerminalLibCmToTerminalItem,
  mapTransportLibCmToTransportItem,
} from "common/utils/mappers";
import { useGetInterface } from "external/sources/interface/interface.queries";
import { useGetNode } from "external/sources/node/node.queries";
import { useGetTerminal } from "external/sources/terminal/terminal.queries";
import { useGetTransport } from "external/sources/transport/transport.queries";
import { Loader } from "features/common/loader";
import { AboutPlaceholder } from "features/explore/about/components/AboutPlaceholder";
import { InterfacePanel } from "features/explore/about/components/interface/InterfacePanel";
import { NodePanel } from "features/explore/about/components/node/NodePanel";
import { TerminalPanel } from "features/explore/about/components/terminal/TerminalPanel";
import { TransportPanel } from "features/explore/about/components/transport/TransportPanel";
import { ExploreSection } from "features/explore/common/ExploreSection";
import { SelectedInfo } from "features/explore/common/selectedInfo";
import { AnimatePresence } from "framer-motion";
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

  const nodeQuery = useGetNode(selected?.type == "node" ? selected?.id : "");
  const terminalQuery = useGetTerminal(selected?.type == "terminal" ? selected?.id : "");
  const transportQuery = useGetTransport(selected?.type == "transport" ? selected?.id : "");
  const interfaceQuery = useGetInterface(selected?.type == "interface" ? selected?.id : "");

  const [showLoader, setShowLoader] = useState(true);

  useEffect(() => {
    const allQueries = [nodeQuery, terminalQuery, transportQuery, interfaceQuery]
    setShowLoader(allQueries.some((x) => x.isFetching));
  }, [nodeQuery, terminalQuery, transportQuery, interfaceQuery]);

  const showPlaceHolder = !showLoader && selected?.type === undefined
  const showNodePanel = !showLoader && selected?.type === "node" && nodeQuery.isSuccess;
  const showTerminalPanel = !showLoader && selected?.type === "terminal" && terminalQuery.isSuccess;
  const showTransportPanel = !showLoader && selected?.type === "transport" && transportQuery.isSuccess;
  const showInterfacePanel = !showLoader && selected?.type === "interface" && interfaceQuery.isSuccess;

  return (
    
    <ExploreSection title={t("about.title")}>
      <AnimatePresence mode={"wait"}>
        {showLoader && <Loader />}
        {showPlaceHolder && <AboutPlaceholder text={t("about.placeholders.item")} />}
        {showNodePanel && <NodePanel key={nodeQuery.data.id} {...mapNodeLibCmToNodeItem(nodeQuery.data)} />}
        {showTerminalPanel && (
          <TerminalPanel key={terminalQuery.data.id} {...mapTerminalLibCmToTerminalItem(terminalQuery.data)} />
        )}
        {showTransportPanel && (
          <TransportPanel key={transportQuery.data.id} {...mapTransportLibCmToTransportItem(transportQuery.data)} />
        )}
        {showInterfacePanel && (
          <InterfacePanel key={interfaceQuery.data.id} {...mapInterfaceLibCmToInterfaceItem(interfaceQuery.data)} />
        )}
      </AnimatePresence>
    </ExploreSection>
  );
};
