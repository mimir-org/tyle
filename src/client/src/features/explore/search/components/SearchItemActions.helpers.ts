import { useDeleteAspectObject, usePatchAspectObjectState } from "external/sources/aspectobject/aspectObject.queries";
import { ItemType } from "../../../entities/types/itemTypes";
import { useDeleteTerminal, usePatchTerminalState } from "external/sources/terminal/terminal.queries";
import { useDeleteUnit, usePatchUnitState } from "external/sources/unit/unit.queries";
import { useDeleteQuantityDatum, usePatchQuantityDatumState } from "external/sources/datum/quantityDatum.queries";
import { useDeleteRds, usePatchRdsState } from "external/sources/rds/rds.queries";
import { useDeleteAttribute, usePatchAttributeState } from "external/sources/attribute/attribute.queries";

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

export function usePatchMutation(item: ItemType) {
  const patchAspectObjectMutation = usePatchAspectObjectState();
  const patchAttributeMutation = usePatchAttributeState();
  const patchQuantityDatumMutation = usePatchQuantityDatumState();
  const patchRdsMutation = usePatchRdsState();
  const patchTerminalMutation = usePatchTerminalState();
  const patchUnitMutation = usePatchUnitState();

  switch (item.kind) {
    case "AspectObjectItem":
      return patchAspectObjectMutation;
    case "AttributeItem":
      return patchAttributeMutation;
    case "QuantityDatumItem":
      return patchQuantityDatumMutation;
    case "RdsItem":
      return patchRdsMutation;
    case "TerminalItem":
      return patchTerminalMutation;
    case "UnitItem":
      return patchUnitMutation;
    default:
      throw new Error("Unknown item kind");
  }
}

export function useDeleteMutation(item: ItemType) {
  const deleteAspectObjectMutation = useDeleteAspectObject(item.id);
  const deleteAttributeMutation = useDeleteAttribute(item.id);
  const deleteQuantityDatumMutation = useDeleteQuantityDatum(item.id);
  const deleteRdsMutation = useDeleteRds(item.id);
  const deleteTerminalMutation = useDeleteTerminal(item.id);
  const deleteUnitMutation = useDeleteUnit(item.id);

  switch (item.kind) {
    case "AspectObjectItem":
      return deleteAspectObjectMutation;
    case "AttributeItem":
      return deleteAttributeMutation;
    case "QuantityDatumItem":
      return deleteQuantityDatumMutation;
    case "RdsItem":
      return deleteRdsMutation;
    case "TerminalItem":
      return deleteTerminalMutation;
    case "UnitItem":
      return deleteUnitMutation;
    default:
      throw new Error("Unknown item kind");
  }
}
