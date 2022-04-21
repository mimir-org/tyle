import { Connector, Interface, Node, Transport } from "../../index";

export const EDGE_KIND = "Edge";

interface Edge {
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

  kind: string;
}

export default Edge;
