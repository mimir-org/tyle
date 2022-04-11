import { MimirorgTokenType } from "../enums/mimirorgTokenType";

export interface MimirorgTokenCm {
  clientId: string;
  tokenType: MimirorgTokenType;
  secret: string;
  validTo: string;
}
