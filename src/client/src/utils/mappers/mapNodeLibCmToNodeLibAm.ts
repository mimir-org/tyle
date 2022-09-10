import { NodeLibAm, NodeLibCm } from "@mimirorg/typelibrary-types";
import { mapNodeTerminalLibCmsToNodeTerminalLibAms } from "./mapNodeTerminalLibCmToNodeTerminalLibAm";

export const mapNodeLibCmToNodeLibAm = (node: NodeLibCm): NodeLibAm => {
  const cm = {
    ...node,
    simpleIdList: node.simples?.map((x) => x.id),
    attributeIdList: node.attributes?.map((x) => x.id),
    nodeTerminals: mapNodeTerminalLibCmsToNodeTerminalLibAms(node.nodeTerminals),
    parentId: node.parentId,
  };
  const { id, firstVersionId, version, ...am } = cm;
  return am as NodeLibAm;
};
