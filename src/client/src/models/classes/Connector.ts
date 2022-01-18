import { Attribute, ConnectorType, EnumBase, RelationType } from "..";

export const CONNECTOR_KIND = "Connector";

class Connector {
  id: string;
  iri: string;
  domain: string;
  name: string;
  type: ConnectorType;
  semanticReference: string;
  nodeId: string;
  nodeIri: string;
  visible: boolean;
  isRequired: boolean;

  // Terminal
  color: string;
  terminalCategoryId: string;
  terminalCategory: EnumBase;
  attributes: Attribute[];
  terminalTypeId: string;

  // Relation
  relationType: RelationType;

  kind: string = CONNECTOR_KIND;

  constructor(connector: Connector) {
    Object.assign(this, connector);
  }
}

export default Connector;
