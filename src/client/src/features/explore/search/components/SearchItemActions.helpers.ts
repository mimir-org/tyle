import { TerminalItem } from "../../../../common/types/terminalItem";
import { AspectObjectItem } from "../../../../common/types/aspectObjectItem";
import { AttributeItem } from "../../../../common/types/attributeItem";

type LinkItem = TerminalItem | AspectObjectItem | AttributeItem;

export function getCloneLink(item: LinkItem) {
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

export function getEditLink(item: LinkItem) {
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
