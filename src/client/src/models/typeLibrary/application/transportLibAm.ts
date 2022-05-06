import { Aspect } from "../enums/aspect";

export interface TransportLibAm {
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
  parentId: string;
}
