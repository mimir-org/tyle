import { MimirorgUserCm } from "./mimirorgUserCm";

export interface MimirorgCompanyCm {
  id: string;
  name: string;
  displayName: string;
  description: string;
  manager: MimirorgUserCm;
}
