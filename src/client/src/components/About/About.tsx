// import { mapBlockLibCmToBlockItem /*, mapTerminalLibCmToTerminalItem*/ } from "common/utils/mappers";
import { useGetBlock } from "api/block.queries";
//import { useGetTerminal } from "external/sources/terminal/terminal.queries";
import { Loader } from "components/Loader";
import { AboutPlaceholder } from "components/About/AboutPlaceholder";
import { BlockPanel } from "components/About/BlockPanel";
//import { TerminalPanel } from "features/explore/about/components/terminal/TerminalPanel";
import { ExploreSection } from "components/ExploreSection/ExploreSection";
import { SelectedInfo } from "components/ExploreSection/selectedInfo";
import { useTranslation } from "react-i18next";
//import { useGetAttribute } from "../../../external/sources/attribute/attribute.queries";
//import AttributePreview from "../../entities/entityPreviews/attribute/AttributePreview";
//import UnitPreview from "../../entities/entityPreviews/unit/UnitPreview";
//import { toUnitLibAm } from "../../entities/units/types/formUnitLib";
//import { useGetUnit } from "../../../external/sources/unit/unit.queries";
//import UnifiedPanel from "./components/common/UnifiedPanel";
//import { useGetAttributeGroup } from "external/sources/attributeGroup/attributeGroup.queries";
//import { AttributeGroupPanel } from "./components/attributeGroup/AttributeGroupPanel";
import { toBlockItem } from "common/utils/mappers/mapBlockLibCmToBlockItem";

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
  const { t: typeName } = useTranslation("entities");

  const blockQuery = useGetBlock(selected?.id ?? "");
  //const terminalQuery = useGetTerminal(selected?.type === "terminal" ? selected?.id : undefined);
  //const attributeQuery = useGetAttribute(selected?.type === "attribute" ? selected?.id : undefined);
  //const attributeGroupQuery = useGetAttributeGroup(selected?.type === "attributeGroup" ? selected?.id : undefined);
  const allQueries = [blockQuery];

  const showLoader = allQueries.some((x) => x.isFetching);

  const showPlaceHolder = !showLoader && selected?.type === undefined;
  const showBlockPanel = !showLoader && selected?.type === "block" && blockQuery.isSuccess;
  //const showTerminalPanel = !showLoader && selected?.type === "terminal" && terminalQuery.isSuccess;
  //const showAttributePanel = !showLoader && selected?.type === "attribute" && attributeQuery.isSuccess;
  //const showAttributeGroupPanel = !showLoader && selected?.type === "attributeGroup" && attributeGroupQuery.isSuccess;

  function typeParser(type?: string) {
    switch (type) {
      case "block":
        return typeName("block.title");
      case "terminal":
        return typeName("terminal.title");
      case "attribute":
        return typeName("attribute.title");
      case "attributeGroup":
        return typeName("attributeGroup.title");
      default:
        return t("about.title");
    }
  }

  return (
    <ExploreSection title={typeParser(selected?.type)}>
      {showLoader && <Loader />}
      {showPlaceHolder && <AboutPlaceholder text={t("about.placeholders.item")} />}
      {showBlockPanel && <BlockPanel key={blockQuery.data.id} {...toBlockItem(blockQuery.data)} />}
      {/*showTerminalPanel && (
        <TerminalPanel
          key={terminalQuery.data.id + terminalQuery.data.kind}
          {...mapTerminalLibCmToTerminalItem(terminalQuery.data)}
        />
      )*/}
      {/*showAttributePanel && (
        <UnifiedPanel {...toAttributeFormFields(attributeQuery.data)}>
          <AttributePreview {...toAttributeFormFields(attributeQuery.data)} />
        </UnifiedPanel>
      )*/}
      {/*showAttributeGroupPanel && (
        <AttributeGroupPanel
          key={attributeGroupQuery.data.id + attributeGroupQuery.data.kind}
          id={attributeGroupQuery.data.id}
          name={attributeGroupQuery.data.name}
          created={attributeGroupQuery.data.created}
          createdBy={attributeGroupQuery.data.createdBy}
          description={attributeGroupQuery.data.description}
          kind={attributeGroupQuery.data.kind}
          attributeIds={attributeGroupQuery.data.attributes.map((x) => x.id)}
          attributes={attributeGroupQuery.data.attributes}
          state={State.Draft}
        />
      )*/}
    </ExploreSection>
  );
};
