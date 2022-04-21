import { Aspect } from "../enums/aspect";

export interface TransportLibAm {
  name: string;
  rdsId: string;
  purposeId: string;
  parentId: string;
  version: string;
  firstVersionId: string;
  aspect: Aspect;
  description: string;
  updatedBy: string;
  updated: string;
  created: string;
  createdBy: string;
  terminalId: string;
  attributeIdList: string[];
}
