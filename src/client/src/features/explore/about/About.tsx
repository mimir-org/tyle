import { mapAspectObjectLibCmToAspectObjectItem, mapTerminalLibCmToTerminalItem } from "common/utils/mappers";
import { useGetAspectObject } from "external/sources/aspectobject/aspectObject.queries";
import { useGetTerminal } from "external/sources/terminal/terminal.queries";
import { Loader } from "features/common/loader";
import { AboutPlaceholder } from "features/explore/about/components/AboutPlaceholder";
import { AspectObjectPanel } from "features/explore/about/components/aspectobject/AspectObjectPanel";
import { TerminalPanel } from "features/explore/about/components/terminal/TerminalPanel";
import { ExploreSection } from "features/explore/common/ExploreSection";
import { SelectedInfo } from "features/explore/common/selectedInfo";
import { useTranslation } from "react-i18next";
import { useGetAttribute } from "../../../external/sources/attribute/attribute.queries";
import AttributePreview from "../../entities/entityPreviews/attribute/AttributePreview";
import { toFormAttributeLib } from "../../entities/attributes/types/formAttributeLib";
import UnitPreview from "../../entities/entityPreviews/unit/UnitPreview";
import QuantityDatumPreview from "../../entities/entityPreviews/quantityDatum/QuantityDatumPreview";
import { RdsPreview } from "../../entities/entityPreviews/rds/RdsPreview";
import { toUnitLibAm } from "../../entities/units/types/formUnitLib";
import { useGetUnit } from "../../../external/sources/unit/unit.queries";
import { useGetQuantityDatum } from "../../../external/sources/datum/quantityDatum.queries";
import { useGetRds } from "../../../external/sources/rds/rds.queries";
import { toFormDatumLib } from "../../entities/quantityDatum/types/formQuantityDatumLib";
import { toFormRdsLib } from "../../entities/RDS/types/formRdsLib";
import UnifiedPanel from "./components/common/UnifiedPanel";

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

  const aspectObjectQuery = useGetAspectObject(selected?.type === "aspectObject" ? selected?.id : undefined);
  const terminalQuery = useGetTerminal(selected?.type === "terminal" ? selected?.id : undefined);
  const attributeQuery = useGetAttribute(selected?.type === "attribute" ? selected?.id : undefined);
  const unitQuery = useGetUnit(selected?.type === "unit" ? selected?.id : undefined);
  const datumQuery = useGetQuantityDatum(selected?.type === "quantityDatum" ? selected?.id : undefined);
  const rdsQuery = useGetRds(selected?.type === "rds" ? selected?.id : undefined);
  const allQueries = [aspectObjectQuery, terminalQuery, attributeQuery, unitQuery, datumQuery, rdsQuery];

  const showLoader = allQueries.some((x) => x.isFetching);

  const showPlaceHolder = !showLoader && selected?.type === undefined;
  const showAspectObjectPanel = !showLoader && selected?.type === "aspectObject" && aspectObjectQuery.isSuccess;
  const showTerminalPanel = !showLoader && selected?.type === "terminal" && terminalQuery.isSuccess;
  const showAttributePanel = !showLoader && selected?.type === "attribute" && attributeQuery.isSuccess;
  const showUnitPanel = !showLoader && selected?.type === "unit" && unitQuery.isSuccess;
  const showDatumPanel = !showLoader && selected?.type === "quantityDatum" && datumQuery.isSuccess;
  const showRdsPanel = !showLoader && selected?.type === "rds" && rdsQuery.isSuccess;

  function typeParser(type?: string) {
    switch (type) {
      case "aspectObject":
        return typeName("aspectObject.title");
      case "terminal":
        return typeName("terminal.title");
      case "attribute":
        return typeName("attribute.title");
      case "unit":
        return typeName("unit.title");
      case "quantityDatum":
        return typeName("quantityDatum.title");
      case "rds":
        return typeName("rds.title");
      default:
        return t("about.title");
    }
  }

  return (
    <ExploreSection title={typeParser(selected?.type)}>
      {showLoader && <Loader />}
      {showPlaceHolder && <AboutPlaceholder text={t("about.placeholders.item")} />}
      {showAspectObjectPanel && (
        <AspectObjectPanel
          key={aspectObjectQuery.data.id + aspectObjectQuery.data.kind}
          {...mapAspectObjectLibCmToAspectObjectItem(aspectObjectQuery.data)}
        />
      )}
      {showTerminalPanel && (
        <TerminalPanel
          key={terminalQuery.data.id + terminalQuery.data.kind}
          {...mapTerminalLibCmToTerminalItem(terminalQuery.data)}
        />
      )}
      {showAttributePanel && (
        <UnifiedPanel {...toFormAttributeLib(attributeQuery.data)}>
          <AttributePreview {...toFormAttributeLib(attributeQuery.data)} />
        </UnifiedPanel>
      )}
      {showUnitPanel && (
        <UnifiedPanel {...toUnitLibAm(unitQuery.data)} state={unitQuery.data.state}>
          <UnitPreview {...toUnitLibAm(unitQuery.data)} state={unitQuery.data.state} />
        </UnifiedPanel>
      )}
      {showDatumPanel && (
        <UnifiedPanel {...toFormDatumLib(datumQuery.data)}>
          <QuantityDatumPreview {...toFormDatumLib(datumQuery.data)} />
        </UnifiedPanel>
      )}
      {showRdsPanel && (
        <UnifiedPanel {...toFormRdsLib(rdsQuery.data)}>
          <RdsPreview {...toFormRdsLib(rdsQuery.data)} />
        </UnifiedPanel>
      )}
    </ExploreSection>
  );
};
