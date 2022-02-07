import { Aspect, Attribute, EnumBase, Purpose, Simple } from "../../index";
import Connector from "./Connector";

export const NODE_KIND = "Node";

interface Node {
  id: string;
  iri: string;
  domain: string;
  projectId: string;
  rds: string;
  description: string;
  semanticReference: string;
  tagNumber: string;
  name: string;
  label: string;
  positionX: number;
  positionY: number;
  purpose: Purpose;

  positionBlockX: number;
  positionBlockY: number;
  level: number;
  order: number;
  statusId: string;
  status: EnumBase;
  createdBy: string;
  libraryTypeId: string;
  created: Date;
  updatedBy: string;
  updated: Date;
  version: string;
  aspect: Aspect;
  isRoot: boolean | false;
  masterProjectId: string;
  masterProjectIri: string;
  symbol: string;
  connectors: Connector[];
  attributes: Attribute[];
  simples: Simple[];
  width: number;
  height: number;
  isLocked: boolean;
  isLockedStatusBy: string;
  isLockedStatusDate: string;
  kind: string;

  // Required only for product aspect
  cost: number;

  // Only for client
  isSelected?: boolean | false;
  isBlockSelected?: boolean | false;
  isHidden?: boolean | false;
}

export default Node;
