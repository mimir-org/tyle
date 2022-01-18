import { Discipline, EnumBase, SelectType } from "..";

export const ATTRIBUTE_KIND = "Attribute";

class Attribute {
  id: string;
  iri: string;
  domain: string;
  entity: string;
  value: string;
  selectedUnitId: string;
  unit: EnumBase;
  qualifierId: string;
  qualifier: EnumBase;
  sourceId: string;
  source: EnumBase;
  conditionId: string;
  condition: EnumBase;
  formatId: string;
  format: EnumBase;
  units: EnumBase[];
  attributeTypeId: string;
  terminalId: string;
  nodeId: string;
  nodeIri: string;
  transportId: string;
  simpleId: string;
  selectValues: string[];
  selectType: SelectType;
  discipline: Discipline;
  tags: Set<string>;
  interfaceId: string;

  isLocked: boolean;
  isLockedStatusBy: string;
  isLockedStatusDate: string;

  kind: string = ATTRIBUTE_KIND;

  // eslint-disable-next-line @typescript-eslint/no-useless-constructor
  constructor(attribute: Attribute) {
    Object.assign(this, attribute);
  }
}

export default Attribute;
