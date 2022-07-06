import { MimirorgUserCm } from "./mimirorgUserCm";

export interface MimirorgCompanyCm {
  id: number;
  name: string;
  displayName: string;
  description: string;
  manager: MimirorgUserCm;
  domain: string;
  logo: string;
  iris: string[];
}
