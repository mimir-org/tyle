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
  contentReferences: string[];
  description: string;
  parentId: string;
}
