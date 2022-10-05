import { NodeLibAm, NodeLibCm } from "@mimirorg/typelibrary-types";
import { mapNodeTerminalLibCmsToNodeTerminalLibAms } from "./mapNodeTerminalLibCmToNodeTerminalLibAm";

export const mapNodeLibCmToNodeLibAm = (node: NodeLibCm): NodeLibAm => ({
  ...node,
  attributeIdList: node.attributes?.map((x) => x.id),
  nodeTerminals: mapNodeTerminalLibCmsToNodeTerminalLibAms(node.nodeTerminals),
  parentId: node.parentId,
});
