import { State } from "@mimirorg/typelibrary-types";

export interface ApprovalAm {
  id: string;
  objectType: string;
  state: State;
  companyId: number;
}
