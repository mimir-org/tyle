import { Aspect } from "../enums/aspect";

export interface InterfaceLibAm {
  name: string;
  rdsName: string;
  rdsCode: string;
  purposeName: string;
  aspect: Aspect;
  companyId: number;
  terminalId: string;
  attributeIdList: string[];
  description: string;
  version: string;
  contentReferences: string[];
  parentId: string;
}
