import { TokenType } from "./tokenType";

export interface TokenView {
  clientId: string;
  tokenType: TokenType;
  secret: string;
  validTo: Date;
}
