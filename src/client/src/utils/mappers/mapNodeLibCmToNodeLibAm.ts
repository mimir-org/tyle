import { NodeLibCm } from "../../models/tyle/client/nodeLibCm";
import { NodeLibAm } from "../../models/tyle/application/nodeLibAm";
import { mapNodeTerminalLibCmsToNodeTerminalLibAms } from "./mapNodeTerminalLibCmToNodeTerminalLibAm";

export const mapNodeLibCmToNodeLibAm = (node: NodeLibCm): NodeLibAm => ({
  ...node,
  simpleIdList: node.simples.map((x) => x.id),
  attributeIdList: node.attributes.map((x) => x.id),
  nodeTerminals: mapNodeTerminalLibCmsToNodeTerminalLibAms(node.nodeTerminals),
  parentId: node.parentIri,
});
