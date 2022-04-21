import { Attribute, ConnectorDirection, ConnectorVisibility, EnumBase, RelationType } from "../../index";

export const CONNECTOR_KIND = "Connector";

interface Connector {
  id: string;
  iri: string;
  domain: string;
  name: string;
  type: ConnectorDirection;
  semanticReference: string;
  nodeId: string;
  nodeIri: string;
  connectorVisibility: ConnectorVisibility;
  isRequired: boolean;

  // Terminal
  color: string;
  terminalCategoryId: string;
  terminalCategory: EnumBase;
  attributes: Attribute[];
  terminalTypeId: string;

  // Relation
  relationType: RelationType;

  kind: string;
}

export default Connector;
