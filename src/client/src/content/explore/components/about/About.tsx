import { AnimatePresence } from "framer-motion";
import { useTranslation } from "react-i18next";
import { useGetAttribute } from "../../../../data/queries/tyle/queriesAttribute";
import { useGetNode } from "../../../../data/queries/tyle/queriesNode";
import { mapNodeLibCmToNodeItem } from "../../../../utils/mappers";
import { mapAttributeLibCmToAttributeItem } from "../../../../utils/mappers/mapAttributeLibCmToAttributeItem";
import { Loader } from "../../../common/Loader";
import { SelectedInfo } from "../../types/selectedInfo";
import { ExploreSection } from "../ExploreSection";
import { AboutPlaceholder } from "./components/AboutPlaceholder";
import { AttributePanel } from "./components/attribute/AttributePanel";
import { NodePanel } from "./components/node/NodePanel";

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
  const attributeQuery = useGetAttribute(selected?.type == "attribute" ? selected?.id : "");
  const currentQuery = selected?.type == "node" ? nodeQuery : attributeQuery;

  const showLoading = currentQuery.isLoading || currentQuery.isFetching;
  const showPlaceHolder = !showLoading && !(currentQuery.isSuccess && currentQuery.data);
  const showNodePanel = !showLoading && nodeQuery.isSuccess && nodeQuery.data;
  const showAttributePanel = !showLoading && attributeQuery.isSuccess && attributeQuery.data;

  return (
    <ExploreSection title={t("title")}>
      <AnimatePresence mode={"wait"}>
        {showLoading && <Loader />}
        {showPlaceHolder && <AboutPlaceholder text={t("placeholders.item")} />}
        {showNodePanel && <NodePanel key={nodeQuery.data.id} {...mapNodeLibCmToNodeItem(nodeQuery.data)} />}
        {showAttributePanel && (
          <AttributePanel key={attributeQuery.data.id} {...mapAttributeLibCmToAttributeItem(attributeQuery.data)} />
        )}
      </AnimatePresence>
    </ExploreSection>
  );
};
