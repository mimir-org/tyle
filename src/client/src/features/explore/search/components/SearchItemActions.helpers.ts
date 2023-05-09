import { ItemType } from "../../../entities/types/itemTypes";

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
    default:
      return "#";
  }
}
