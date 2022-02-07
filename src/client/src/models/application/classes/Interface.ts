import { Attribute, Connector } from "./index";

export const INTERFACE_KIND = "Interface";

interface Interface {
  id: string;
  iri: string;
  version: string;
  rds: string;
  name: string;
  label: string;
  description: string;
  statusId: string;
  semanticReference: string;
  attributes: Attribute[];
  inputTerminalId: string;
  inputTerminal: Connector;
  outputTerminalId: string;
  outputTerminal: Connector;
  updatedBy: string;
  updated: Date;
  createdBy: string;
  created: Date;
  libraryTypeId: string;
  kind: string;
}

export default Interface;
