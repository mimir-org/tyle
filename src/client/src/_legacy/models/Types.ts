import { Aspect, Attribute, Connector, ObjectType, Simple } from "./index";
import { ConnectorDirection, Discipline, SelectType } from "./Enums";

export interface AttributeType {
  id: string;
  entity: string;
  aspect: Aspect;
  qualifierId: string;
  qualifier: EnumBase;
  sourceId: string;
  source: EnumBase;
  conditionId: string;
  condition: EnumBase;
  formatId: string;
  format: EnumBase;
  units: EnumBase[];
  tags: Set<string>;
  description: string;
  selectValues: string[];
  selectType: SelectType;
  discipline: Discipline;
}

export interface EnumBase {
  id: string;
  name: string;
  description: string;
  semanticReference: string;
  color: string;
}

export interface Library {
  objectBlocks: LibItem[];
  interfaces: LibItem[];
  transports: LibItem[];
}

export interface LibItem {
  id: string;
  rds: string;
  category: string;
  name: string;
  label: string;
  description: string;
  aspect: Aspect;
  connectors: Connector[];
  attributes?: Attribute[] | null;
  simples?: Simple[] | null;
  semanticReference: string;
  statusId: string;
  version: string;
  symbolId: string;
  terminalId: string;
  terminalTypeId: string;
  libraryType: ObjectType;
  purpose: Purpose;
  updatedBy: string;
  updated: Date;
  createdBy: string;
  created: Date;
  libraryTypeId: string;
}

export interface LibrarySubProjectItem {
  id: string;
  name: string;
  version: string;
  description: string;
  projectOwner: string;
  updated: Date;
  updatedBy: string;
}

export interface Rds {
  id: string;
  name: string;
  code: string;
  rdsCategoryId: string;
  rdsCategory: EnumBase;
  semanticReference: string;
  aspect: Aspect;
}

export interface TerminalType {
  id: string;
  name: string;
  color: string;
  terminalCategoryId: string;
  terminalCategory: EnumBase;
  semanticReference: string;
  attributes: AttributeType[];
}

export interface TerminalTypeItem {
  terminalId: string;
  terminalTypeId: string;
  selected: boolean | false;
  connectorType: ConnectorDirection;
  number: number;
  categoryId: string;
}

export type TerminalTypeDict = TerminalTypeDictItem[];

export interface TerminalTypeDictItem {
  key: string;
  value: TerminalType[];
}

export interface Purpose {
  id: string;
  name: string;
  discipline: Discipline;
  description: string;
  semanticReference: string;
}

export interface PredefinedAttribute {
  key: string;
  values: Record<string, boolean>;
  isMultiSelect: boolean;
}

export interface LocationType {
  id: string;
  name: string;
  description: string;
  semanticReference: string;
  locationSubTypes: LocationType[];
}

export interface User {
  name: string;
  email: string;
  role: string;
}

export interface BlobData {
  id: string;
  name: string;
  data: string;
  discipline: Discipline;
}

export interface SimpleTypeResponse {
  id: string;
  name: string;
  semanticReference: string;
  attributeTypes: AttributeType[];
}

export interface SimpleType {
  id: string;
  name: string;
  semanticReference: string;
  attributes: AttributeType[];
}

export interface LibraryCategory {
  name: string;
  nodes: LibItem[];
}