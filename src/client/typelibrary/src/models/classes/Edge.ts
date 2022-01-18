import { Aspect, Connector, Interface, Node, Transport } from "..";

export type isEdge = keyof Edge;
export const EDGE_KIND = "Edge";

class Edge {
  id: string;
  iri: string;
  domain: string;
  projectId: string;
  fromConnectorId: string;
  fromConnector: Connector;

  toConnectorId: string;
  toConnector: Connector;

  fromNodeId: string;
  fromNode: Node;

  toNodeId: string;
  toNode: Node;

  fromConnectorIri: string;
  toConnectorIri: string;
  fromNodeIri: string;
  toNodeIri: string;

  transportId: string;
  transport: Transport;

  interfaceId: string;
  interface: Interface;

  isHidden: boolean | false;
  masterProjectId: string;
  masterProjectIri: string;
  isSelected: boolean;

  isLocked: boolean;
  isLockedStatusBy: string;
  isLockedStatusDate: string;

  kind: string = EDGE_KIND;

  constructor(edge: Edge) {
    Object.assign(this, edge);
  }

  parentType() {
    if (this.fromNode) return this.fromNode.aspect;

    return Aspect.NotSet;
  }

  targetType() {
    if (this.toNode) return this.toNode.aspect;

    return Aspect.NotSet;
  }
}

export default Edge;
