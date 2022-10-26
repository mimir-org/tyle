import { AnimatePresence } from "framer-motion";
import { useTranslation } from "react-i18next";
import { useGetInterface } from "../../../../data/queries/tyle/queriesInterface";
import { useGetNode } from "../../../../data/queries/tyle/queriesNode";
import { useGetTerminal } from "../../../../data/queries/tyle/queriesTerminal";
import { useGetTransport } from "../../../../data/queries/tyle/queriesTransport";
import {
  mapInterfaceLibCmToInterfaceItem,
  mapNodeLibCmToNodeItem,
  mapTerminalLibCmToTerminalItem,
  mapTransportLibCmToTransportItem,
} from "../../../../utils/mappers";
import { Loader } from "../../../../content/common/loader";
import { SelectedInfo } from "../../types/selectedInfo";
import { ExploreSection } from "../ExploreSection";
import { AboutPlaceholder } from "./components/AboutPlaceholder";
import { InterfacePanel } from "./components/interface/InterfacePanel";
import { NodePanel } from "./components/node/NodePanel";
import { TerminalPanel } from "./components/terminal/TerminalPanel";
import { TransportPanel } from "./components/transport/TransportPanel";

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
  const showNodePanel = nodeQuery.isSuccess && nodeQuery.data;

  const terminalQuery = useGetTerminal(selected?.type == "terminal" ? selected?.id : "");
  const showTerminalPanel = terminalQuery.isSuccess && terminalQuery.data;

  const transportQuery = useGetTransport(selected?.type == "transport" ? selected?.id : "");
  const showTransportPanel = transportQuery.isSuccess && transportQuery.data;

  const interfaceQuery = useGetInterface(selected?.type == "interface" ? selected?.id : "");
  const showInterfacePanel = interfaceQuery.isSuccess && interfaceQuery.data;

  const allQueries = [nodeQuery, terminalQuery, transportQuery, interfaceQuery];

  const showLoading = allQueries.some((x) => x.isLoading);
  const showPlaceHolder = allQueries.every((x) => x.isIdle);

  return (
    <ExploreSection title={t("title")}>
      <AnimatePresence mode={"wait"}>
        {showLoading && <Loader />}
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
