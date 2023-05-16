import { usePatchTerminalState } from "external/sources/terminal/terminal.queries";
import { ItemType } from "../../../entities/types/itemTypes";
import { usePatchAspectObjectState } from "external/sources/aspectobject/aspectObject.queries";
import { usePatchAttributeState } from "external/sources/attribute/attribute.queries";

export function usePatchMutation(item: ItemType) {
  const patchTerminalState = usePatchTerminalState();
  const patchAspectObjectState = usePatchAspectObjectState();
  const patchAttributeState = usePatchAttributeState();
  const patchUnitState = usePatchUnitState();
  const patchQuantityDatumState = usePatchQuantityDatumState();
  const patchRdsState = usePatchRdsState();

  switch (item.kind) {
    case "TerminalItem":
      return patchTerminalState;
    case "AspectObjectItem":
      return patchAspectObjectState;
    case "AttributeItem":
      return patchAttributeState;
    case "UnitItem":
      return patchUnitState;
    case "QuantityDatumItem":
      return patchQuantityDatumState;
    case "RdsItem":
      return patchRdsState;
  }
}

export function getCloneLink(item: ItemType) {
  switch (item.kind) {
    case "TerminalItem":
      return `/form/terminal/clone/${item.id}`;
    case "AspectObjectItem":
      return `/form/aspectobject/clone/${item.id}`;
    case "AttributeItem":
      return `/form/attribute/clone/${item.id}`;
    case "UnitItem":
      return `/form/unit/clone/${item.id}`;
    case "QuantityDatumItem":
      return `/form/quantityDatum/clone/${item.id}`;
    case "RdsItem":
      return `/form/rds/clone/${item.id}`;
    default:
      return "#";
  }
}

export function getEditLink(item: ItemType) {
  switch (item.kind) {
    case "TerminalItem":
      return `/form/terminal/edit/${item.id}`;
    case "AspectObjectItem":
      return `/form/aspectobject/edit/${item.id}`;
    case "AttributeItem":
      return `/form/attribute/edit/${item.id}`;
    case "UnitItem":
      return `/form/unit/edit/${item.id}`;
    case "QuantityDatumItem":
      return `/form/quantityDatum/edit/${item.id}`;
    case "RdsItem":
      return `/form/rds/edit/${item.id}`;
    default:
      return "#";
  }
}
