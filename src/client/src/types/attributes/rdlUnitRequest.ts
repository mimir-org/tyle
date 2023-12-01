import { RdlObjectRequest } from "../common/rdlObjectRequest";

export interface RdlUnitRequest extends RdlObjectRequest {
  symbol: string | null;
}
