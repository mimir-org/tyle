import { AnimatePresence } from "framer-motion";
import { useTranslation } from "react-i18next";
import { useGetAttribute } from "../../../../data/queries/tyle/queriesAttribute";
import { useGetNode } from "../../../../data/queries/tyle/queriesNode";
import { useGetTerminal } from "../../../../data/queries/tyle/queriesTerminal";
import { mapNodeLibCmToNodeItem, mapTerminalLibCmToTerminalItem } from "../../../../utils/mappers";
import { mapAttributeLibCmToAttributeItem } from "../../../../utils/mappers/mapAttributeLibCmToAttributeItem";
import { Loader } from "../../../common/Loader";
import { SelectedInfo } from "../../types/selectedInfo";
import { ExploreSection } from "../ExploreSection";
import { AboutPlaceholder } from "./components/AboutPlaceholder";
import { AttributePanel } from "./components/attribute/AttributePanel";
import { NodePanel } from "./components/node/NodePanel";
import { TerminalPanel } from "./components/terminal/TerminalPanel";

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

  const attributeQuery = useGetAttribute(selected?.type == "attribute" ? selected?.id : "");
  const showAttributePanel = attributeQuery.isSuccess && attributeQuery.data;

  const terminalQuery = useGetTerminal(selected?.type == "terminal" ? selected?.id : "");
  const showTerminalPanel = terminalQuery.isSuccess && terminalQuery.data;

  const allQueries = [nodeQuery, attributeQuery, terminalQuery];
  const showLoading = allQueries.some((x) => x.isLoading);
  const showPlaceHolder = allQueries.every((x) => x.isIdle);

  return (
    <ExploreSection title={t("title")}>
      <AnimatePresence mode={"wait"}>
        {showLoading && <Loader />}
        {showPlaceHolder && <AboutPlaceholder text={t("placeholders.item")} />}
        {showNodePanel && <NodePanel key={nodeQuery.data.id} {...mapNodeLibCmToNodeItem(nodeQuery.data)} />}
        {showAttributePanel && (
          <AttributePanel key={attributeQuery.data.id} {...mapAttributeLibCmToAttributeItem(attributeQuery.data)} />
        )}
        {showTerminalPanel && (
          <TerminalPanel key={terminalQuery.data.id} {...mapTerminalLibCmToTerminalItem(terminalQuery.data)} />
        )}
      </AnimatePresence>
    </ExploreSection>
  );
};
