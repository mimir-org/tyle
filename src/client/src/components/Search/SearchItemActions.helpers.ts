import { useDeleteAttribute, usePatchAttributeState } from "api/attribute.queries";
import { useDeleteBlock, usePatchBlockState } from "api/block.queries";
import { useDeleteTerminal, usePatchTerminalState } from "api/terminal.queries";
import { ItemType } from "types/itemTypes";

export function getCloneLink(item: ItemType) {
  switch (item.kind) {
    case "TerminalItem":
      return `/form/terminal/clone/${item.id}`;
    case "BlockItem":
      return `/form/block/clone/${item.id}`;
    case "AttributeItem":
      return `/form/attribute/clone/${item.id}`;
    case "AttributeGroupItem":
      return `/form/attributeGroup/clone/${item.id}`;
    default:
      return "#";
  }
}

export function getEditLink(item: ItemType) {
  switch (item.kind) {
    case "TerminalItem":
      return `/form/terminal/edit/${item.id}`;
    case "BlockItem":
      return `/form/block/edit/${item.id}`;
    case "AttributeItem":
      return `/form/attribute/edit/${item.id}`;
    case "AttributeGroupItem":
      return `/form/attributeGroup/edit/${item.id}`;
    default:
      return "#";
  }
}

export function usePatchMutation(item: ItemType) {
  const patchBlockMutation = usePatchBlockState(item.id);
  const patchAttributeMutation = usePatchAttributeState(item.id);
  const patchTerminalMutation = usePatchTerminalState(item.id);
  //const patchAttributeGroup = usePatchUnitState();

  switch (item.kind) {
    case "BlockItem":
      return patchBlockMutation;
    case "AttributeItem":
      return patchAttributeMutation;
    //case "AttributeGroupItem":
    //return patchAttributeGroup;
    case "TerminalItem":
      return patchTerminalMutation;
    default:
      throw new Error("Unknown item kind");
  }
}

export function useDeleteMutation(item: ItemType) {
  const deleteBlockMutation = useDeleteBlock(item.id);
  const deleteAttributeMutation = useDeleteAttribute(item.id);
  const deleteTerminalMutation = useDeleteTerminal(item.id);
  //const deleteAttributeGroupMutation = useDeleteAttributeGroup(item.id);

  switch (item.kind) {
    case "BlockItem":
      return deleteBlockMutation;
    case "AttributeItem":
      return deleteAttributeMutation;
    /*case "AttributeGroupItem":
      return deleteAttributeGroupMutation;*/
    case "TerminalItem":
      return deleteTerminalMutation;
    default:
      throw new Error("Unknown item kind");
  }
}
