import { State } from "./state";

export interface ImfType {
  id: string;
  name: string;
  description: string | null;
  version: string;
  createdOn: Date;
  createdBy: string;
  contributedBy: string[];
  lastUpdateOn: Date;
  state: State;
}
