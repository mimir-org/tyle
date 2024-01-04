import { useGetBlock } from "api/block.queries";
import ExploreSection from "components/ExploreSection";
import Loader from "components/Loader";
import { toAttributeItem, toBlockItem, toTerminalItem } from "helpers/mappers.helpers";
import { SelectedInfo } from "types/selectedInfo";
import AboutPlaceholder from "./AboutPlaceholder";
import BlockPanel from "./BlockPanel";
import { TerminalPanel } from "./TerminalPanel";
import { useGetTerminal } from "../../api/terminal.queries";
import { useGetAttribute } from "../../api/attribute.queries";
import AttributePanel from "./AttributePanel";

interface AboutProps {
  selected?: SelectedInfo;
}

/**
 * Component which houses info-panels that display various data associated with the selected item.
 *
 * @param selected the currently selected item
 * @constructor
 */
const About = ({ selected }: AboutProps) => {
  const blockQuery = useGetBlock(selected?.type === "block" ? selected?.id : "");
  const terminalQuery = useGetTerminal(selected?.type === "terminal" ? selected?.id : "");
  const attributeQuery = useGetAttribute(selected?.type === "attribute" ? selected?.id : "");
  const allQueries = [blockQuery, terminalQuery, attributeQuery];

  const showLoader = allQueries.some((x) => x.isFetching);

  const showPlaceHolder = !showLoader && selected?.type === undefined;
  const showBlockPanel = !showLoader && selected?.type === "block" && blockQuery.isSuccess;
  const showTerminalPanel = !showLoader && selected?.type === "terminal" && terminalQuery.isSuccess;
  const showAttributePanel = !showLoader && selected?.type === "attribute" && attributeQuery.isSuccess;

  function typeParser(type?: string) {
    switch (type) {
      case "block":
        return "Block";
      case "terminal":
        return "Terminal";
      case "attribute":
        return "Attribute";
      default:
        return "About";
    }
  }

  return (
    <ExploreSection title={typeParser(selected?.type)}>
      {showLoader && <Loader />}
      {showPlaceHolder && <AboutPlaceholder text="Select an item to view its properties" />}
      {showBlockPanel && <BlockPanel key={blockQuery.data.id} {...toBlockItem(blockQuery.data)} />}
      {showTerminalPanel && <TerminalPanel key={terminalQuery.data.id} {...toTerminalItem(terminalQuery.data)} />}
      {showAttributePanel && <AttributePanel key={attributeQuery.data.id} {...toAttributeItem(attributeQuery.data)} />}
    </ExploreSection>
  );
};

export default About;
