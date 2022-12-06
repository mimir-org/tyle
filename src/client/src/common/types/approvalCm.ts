import { State } from "@mimirorg/typelibrary-types";

export interface ApprovalCm {
    id: string;
    name: string;
    description: string;
    objectType: string;
    state: State;
    companyId: number;
    companyName: string;
    userId: string;
    userName:string;
  }
  