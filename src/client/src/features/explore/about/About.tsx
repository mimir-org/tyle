import { Loader } from "common/components/loader";
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
import { AboutPlaceholder } from "features/explore/about/components/AboutPlaceholder";
import { InterfacePanel } from "features/explore/about/components/interface/InterfacePanel";
import { NodePanel } from "features/explore/about/components/node/NodePanel";
import { TerminalPanel } from "features/explore/about/components/terminal/TerminalPanel";
import { TransportPanel } from "features/explore/about/components/transport/TransportPanel";
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
  const { t } = useTranslation("translation", { keyPrefix: "about" });

  const nodeQuery = useGetNode(selected?.type == "node" ? selected?.id : "");
  const terminalQuery = useGetTerminal(selected?.type == "terminal" ? selected?.id : "");
  const transportQuery = useGetTransport(selected?.type == "transport" ? selected?.id : "");
  const interfaceQuery = useGetInterface(selected?.type == "interface" ? selected?.id : "");
  const allQueries = [nodeQuery, terminalQuery, transportQuery, interfaceQuery];

  const showLoader = allQueries.some((x) => x.isFetching);
  const showPlaceHolder = !showLoader && allQueries.every((x) => !x.isFetched);

  const showNodePanel = !showLoader && nodeQuery.isSuccess;
  const showTerminalPanel = !showLoader && terminalQuery.isSuccess;
  const showTransportPanel = !showLoader && transportQuery.isSuccess;
  const showInterfacePanel = !showLoader && interfaceQuery.isSuccess;

  return (
    <ExploreSection title={t("title")}>
      <AnimatePresence mode={"wait"}>
        {showLoader && <Loader />}
        {showPlaceHolder && <AboutPlaceholder text={t("placeholders.item")} />}
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
