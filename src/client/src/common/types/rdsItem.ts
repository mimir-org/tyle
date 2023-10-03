import { StateItem } from "./stateItem";

export interface RdsItem extends StateItem {
  id: string;
  name: string;
  description: string;
  kind: string;
  rdsCode: string;
  createdBy: string;
}
