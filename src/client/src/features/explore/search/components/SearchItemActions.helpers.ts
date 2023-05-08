import { TerminalItem } from "../../../../common/types/terminalItem";
import { AspectObjectItem } from "../../../../common/types/aspectObjectItem";
import { AttributeItem } from "../../../../common/types/attributeItem";

export function getCloneLink(item: TerminalItem | AspectObjectItem | AttributeItem) {
  switch (item.kind) {
    case "TerminalItem":
      return `/form/terminal/clone/${item.id}`;
    case "AspectObjectItem":
      return `/form/aspectobject/clone/${item.id}`;
    case "AttributeItem":
      return `/form/attribute/clone/${item.id}`;
    default:
      return "#";
  }
}

export function getEditLink(item: TerminalItem | AspectObjectItem | AttributeItem) {
  switch (item.kind) {
    case "TerminalItem":
      return `/form/terminal/edit/${item.id}`;
    case "AspectObjectItem":
      return `/form/aspectobject/edit/${item.id}`;
    case "AttributeItem":
      return `/form/attribute/edit/${item.id}`;
    default:
      return "#";
  }
}
